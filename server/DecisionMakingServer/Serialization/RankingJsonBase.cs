using DecisionMakingServer.APIModels;

namespace DecisionMakingServer.Serialization;

public class RankingJsonBase : RankingDTO
{
    public List<UserRankingJsonBase> Users { get; set; } = new();
    public List<AnswerJsonBase> Answers { get; set; } = new();
}