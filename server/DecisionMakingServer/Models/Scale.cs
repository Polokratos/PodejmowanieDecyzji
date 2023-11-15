namespace DecisionMakingServer.Models;

public class Scale
{
    public int ScaleId { get; set; }
    
    public int RankingId { get; set; }
    public Ranking Ranking { get; set; } = null!;
    
    public List<ScaleValue> ScaleValues = new();
}