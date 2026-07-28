using System.Collections.Generic;

namespace DeepwaterEngagementSuite;

public class VoyageProfile
{
    public List<VoyageBorderModifier> BorderModifiers { get; set; } = [];
    public List<VoyageChartModifier> ChartModifiers { get; set; } = [];
}
