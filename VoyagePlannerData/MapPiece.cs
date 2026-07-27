using System.Collections.Generic;

namespace DeepwaterEngagementSuite.VoyagePlannerData;

public enum PieceType
{
    Cross,
    Straight,
    Corner,
    Tee,
    Single,
}

public record MapPiece(
    int Id,
    PieceType Type,
    Direction BaseConnections,
    List<Modifier> Modifiers)
{
    public int DistinctRotations => Type switch
    {
        PieceType.Cross => 1,
        PieceType.Straight => 2,
        PieceType.Corner => 4,
        PieceType.Tee => 4,
        PieceType.Single => 4,
        _ => 4
    };

    public Direction GetConnections(int rotation)
    {
        return BaseConnections.RotateCcw(rotation);
    }
}
