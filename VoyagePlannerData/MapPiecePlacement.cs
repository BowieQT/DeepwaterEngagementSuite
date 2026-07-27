namespace DeepwaterEngagementSuite.VoyagePlannerData;

public record MapPiecePlacement(
    MapPiece Piece,
    int Rotation,
    Direction Connections);
