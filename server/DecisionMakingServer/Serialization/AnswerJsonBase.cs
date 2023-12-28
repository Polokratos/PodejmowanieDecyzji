namespace DecisionMakingServer.Serialization;

public class AnswerJsonBase
{
    public int AnswerId { get; set; }
    public int UserId { get; set; }
    public int CriterionId { get; set; }
    public int LeftAlternativeId { get; set; }
    public int RightAlternativeId { get; set; }
    public float Value { get; set; }
}