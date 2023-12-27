using DecisionMakingServer.APIModels;

namespace DecisionMakingServer.Models.NonDbModels;

public class UserRankingInfo
{
    public int RankingId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public UserRole Role;
}


public static class UserRankingInfoExtensions
{
    public static RankingHeaderDTO ToRankingHeaderDto(this UserRankingInfo uri)
    {
        return new RankingHeaderDTO
        {
            Id = uri.RankingId,
            Name = uri.Name,
            Description = uri.Description,
            Role = uri.Role
        };
    }
} 