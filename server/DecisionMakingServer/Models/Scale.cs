namespace DecisionMakingServer.Models;

public class Scale
{
    public int ScaleId { get; set; }
    public User User { get; set; } = null!;
    public DateTime CreationDate { get; set; } = DateTime.Now;

    public List<ScaleValue> ScaleValues = new();
}