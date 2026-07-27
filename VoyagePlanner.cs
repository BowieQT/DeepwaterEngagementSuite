using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DeepwaterEngagementSuite.VoyagePlannerData;

namespace DeepwaterEngagementSuite;

public class VoyagePlanner
{
    private const int GridSize = 3;

    private readonly (Direction Dir, int Dr, int Dc)[] _directions =
    [
        (Direction.Up, 1, 0),
        (Direction.Down, -1, 0),
        (Direction.Left, 0, -1),
        (Direction.Right, 0, 1)
    ];

    private MapPiecePlacement[,] _grid;
    private bool[] _pieceUsed;
    private double _bestScore;
    private List<VoyageSolution> _topSolutions;
    private long _nodesExplored;
    private long _nodesPruned;
    private Stopwatch _stopwatch;
    private HashSet<(int R, int C)> _lockedCells;
    private Dictionary<(int R, int C), (int PieceIdx, int Rotation)> _lockedAssignments;
    private (int R, int C)[] _cellOrder;
    private VoyagePuzzle _puzzle;
    private double _maxModifierPerPiece;
    private bool _cancelled;

    public IEnumerable<VoyageSolutionResult> Solve(VoyagePuzzle puzzle, VoyagePlannerSettings settings = null)
    {
        settings ??= new VoyagePlannerSettings();
        _puzzle = puzzle;
        _grid = new MapPiecePlacement[GridSize, GridSize];
        _pieceUsed = new bool[puzzle.AvailablePieces.Count];
        _bestScore = double.NegativeInfinity;
        _topSolutions = new List<VoyageSolution>(settings.TopN);
        _nodesExplored = 0;
        _nodesPruned = 0;
        _stopwatch = Stopwatch.StartNew();
        _cancelled = false;

        _lockedCells = puzzle.LockedPlacements
            .Select(lp => (lp.Row, lp.Col))
            .ToHashSet();
        _lockedAssignments = puzzle.LockedPlacements
            .ToDictionary(
                lp => (lp.Row, lp.Col),
                lp => (puzzle.AvailablePieces.IndexOf(puzzle.AvailablePieces.First(p => p.Id == lp.PieceId)), lp.Rotation));

        _cellOrder = BuildCellOrder(puzzle);

        _maxModifierPerPiece = puzzle.AvailablePieces
            .Select(p => p.Modifiers.Sum(m => m.Weight))
            .DefaultIfEmpty(0)
            .Max();

        var results = Search(0, settings);

        foreach (var result in results)
        {
            if (_cancelled) yield break;
            yield return result;
        }

        yield return FinalResult();
    }

    public void Cancel() => _cancelled = true;

    private (int R, int C)[] BuildCellOrder(VoyagePuzzle puzzle)
    {
        var order = new List<(int R, int C)>();

        foreach (var (r, c) in _lockedCells)
        {
            order.Add((r, c));
        }

        for (var r = 0; r < GridSize; r++)
        {
            for (var c = 0; c < GridSize; c++)
            {
                if (!_lockedCells.Contains((r, c)))
                {
                    order.Add((r, c));
                }
            }
        }

        return order.ToArray();
    }

    private IEnumerable<VoyageSolutionResult> Search(int idx, VoyagePlannerSettings settings)
    {
        if (_cancelled) yield break;

        if (settings.TimeLimitSeconds.HasValue &&
            _stopwatch.Elapsed.TotalSeconds >= settings.TimeLimitSeconds.Value)
        {
            yield break;
        }

        if (idx == GridSize * GridSize)
        {
            if (IsFullyConnected())
            {
                var score = CalculateScore(_puzzle);
                if (score > _bestScore)
                {
                    _bestScore = score;
                    var solution = new VoyageSolution(
                        CloneGrid(),
                        score,
                        true);
                    _topSolutions.Insert(0, solution);
                    if (_topSolutions.Count > settings.TopN)
                    {
                        _topSolutions.RemoveAt(_topSolutions.Count - 1);
                    }

                    if (settings.YieldIntermediate)
                    {
                        yield return new VoyageSolutionResult(
                            new List<VoyageSolution>(_topSolutions),
                            _nodesExplored,
                            _nodesPruned);
                    }
                }
            }

            yield break;
        }

        var (r, c) = _cellOrder[idx];

        if (_lockedCells.Contains((r, c)))
        {
            var (pieceIdx, rotation) = _lockedAssignments[(r, c)];
            var piece = _puzzle.AvailablePieces[pieceIdx];
            var connections = piece.GetConnections(rotation);
            _grid[r, c] = new MapPiecePlacement(piece, rotation, connections);
            _pieceUsed[pieceIdx] = true;

            if (CheckAdjacency(r, c))
            {
                foreach (var result in Search(idx + 1, settings))
                {
                    yield return result;
                }
            }

            _pieceUsed[pieceIdx] = false;
            _grid[r, c] = null;
            yield break;
        }

        var upperBoundScore = CalculateUpperBoundScore();
        if (upperBoundScore <= _bestScore)
        {
            _nodesPruned++;
            yield break;
        }

        for (var pieceIdx = 0; pieceIdx < _pieceUsed.Length; pieceIdx++)
        {
            if (_cancelled) yield break;
            if (_pieceUsed[pieceIdx]) continue;

            _nodesExplored++;

            var piece = _puzzle.AvailablePieces[pieceIdx];

            for (var rot = 0; rot < piece.DistinctRotations; rot++)
            {
                var connections = piece.GetConnections(rot);

                if (!CheckAdjacency(r, c, connections))
                {
                    _nodesPruned++;
                    continue;
                }

                _grid[r, c] = new MapPiecePlacement(piece, rot, connections);
                _pieceUsed[pieceIdx] = true;

                foreach (var result in Search(idx + 1, settings))
                {
                    yield return result;
                }

                _pieceUsed[pieceIdx] = false;
                _grid[r, c] = null;
            }
        }
    }

    private bool CheckAdjacency(int r, int c, Direction? connections = null)
    {
        var conn = connections ?? _grid[r, c].Connections;

        foreach (var (dir, dr, dc) in _directions)
        {
            var nr = r + dr;
            var nc = c + dc;

            if (nr < 0 || nr >= GridSize || nc < 0 || nc >= GridSize) continue;
            if (_grid[nr, nc] == null) continue;

            var neighborConn = _grid[nr, nc].Connections;
            var hasConnection = conn.HasFlag(dir);
            var neighborHasConnection = neighborConn.HasFlag(dir.Opposite());

            if (hasConnection != neighborHasConnection)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsFullyConnected()
    {
        var visited = new bool[GridSize, GridSize];
        var stack = new Stack<(int R, int C)>();

        stack.Push((0, 0));
        visited[0, 0] = true;
        var count = 1;

        while (stack.TryPop(out var pos))
        {
            var (cr, cc) = pos;
            var conn = _grid[cr, cc].Connections;

            foreach (var (dir, dr, dc) in _directions)
            {
                if (!conn.HasFlag(dir)) continue;

                var nr = cr + dr;
                var nc = cc + dc;

                if (nr < 0 || nr >= GridSize || nc < 0 || nc >= GridSize) continue;
                if (visited[nr, nc]) continue;

                var neighborConn = _grid[nr, nc].Connections;
                if (!neighborConn.HasFlag(dir.Opposite())) continue;

                visited[nr, nc] = true;
                count++;
                stack.Push((nr, nc));
            }
        }

        return count == GridSize * GridSize;
    }

    private double CalculateScore(VoyagePuzzle puzzle)
    {
        var score = 0.0;

        for (var r = 0; r < GridSize; r++)
        {
            for (var c = 0; c < GridSize; c++)
            {
                var cellScore = 0.0;

                foreach (var (dir, dr, dc) in _directions)
                {
                    var nr = r + dr;
                    var nc = c + dc;

                    if (nr < 0 || nr >= GridSize || nc < 0 || nc >= GridSize) continue;
                    if (_grid[nr, nc] == null) continue;

                    var neighbor = _grid[nr, nc];
                    cellScore += neighbor.Piece.Modifiers.Sum(modifier => modifier.Weight);
                }

                score += cellScore * puzzle.LocationModifiers[r, c];
            }
        }

        return score;
    }

    private double CalculateUpperBoundScore()
    {
        var score = 0.0;
        var filledCount = 0;

        for (var i = 0; i < GridSize; i++)
        {
            for (var j = 0; j < GridSize; j++)
            {
                if (_grid[i, j] != null)
                {
                    filledCount++;
                    var cellScore = 0.0;

                    foreach (var (dir, dr, dc) in _directions)
                    {
                        var nr = i + dr;
                        var nc = j + dc;

                        if (nr < 0 || nr >= GridSize || nc < 0 || nc >= GridSize) continue;

                        if (_grid[nr, nc] != null)
                        {
                            cellScore += _grid[nr, nc].Piece.Modifiers.Sum(modifier => modifier.Weight);
                        }
                        else
                        {
                            cellScore += _maxModifierPerPiece;
                        }
                    }

                    score += cellScore * _puzzle.LocationModifiers[i, j];
                }
            }
        }

        var emptyCount = GridSize * GridSize - filledCount;
        var maxNeighbors = 4;
        var maxLocMod = 0.0;
        for (var i = 0; i < GridSize; i++)
        {
            for (var j = 0; j < GridSize; j++)
            {
                maxLocMod = Math.Max(maxLocMod, _puzzle.LocationModifiers[i, j]);
            }
        }

        score += emptyCount * maxNeighbors * _maxModifierPerPiece * maxLocMod;

        return score;
    }

    private MapPiecePlacement[,] CloneGrid()
    {
        var clone = new MapPiecePlacement[GridSize, GridSize];
        for (var i = 0; i < GridSize; i++)
        {
            for (var j = 0; j < GridSize; j++)
            {
                clone[i, j] = _grid[i, j];
            }
        }

        return clone;
    }

    private VoyageSolutionResult FinalResult()
    {
        return new VoyageSolutionResult(
            new List<VoyageSolution>(_topSolutions),
            _nodesExplored,
            _nodesPruned);
    }
}
