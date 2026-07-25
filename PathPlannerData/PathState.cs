using System.Collections.Generic;
using GameOffsets.Native;

namespace DeepwaterEngagementSuite.PathPlannerData;

public record PathState(List<Vector2i> Points, double Score);