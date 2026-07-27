using System.Collections.Generic;

namespace DeepwaterEngagementSuite.VoyagePlannerData;

public record VoyagePuzzle(
    List<MapPiece> AvailablePieces,
    double[,] LocationModifiers,
    List<LockedPlacement> LockedPlacements);
