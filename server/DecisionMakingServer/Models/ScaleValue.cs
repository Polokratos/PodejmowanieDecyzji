namespace DecisionMakingServer.Models;

public class ScaleValue
{
    public int ScaleValueID { get; set; }
    public Scale Scale { get; set; } = null!;
    public int Value { get; set; } // Maximum value of given interval
    public string Description { get; set; } = string.Empty;
}