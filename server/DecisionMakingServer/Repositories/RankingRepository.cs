using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using DecisionMakingServer.Enums;
using DecisionMakingServer.Models;
using DecisionMakingServer.Models.NonDbModels;
using DecisionMakingServer.Serialization;

namespace DecisionMakingServer.Repositories;

public class RankingRepository : AbstractDbRepository
{
    public IEnumerable<Ranking> GetUserRankings(int userId)
    {
        return DbContext.UserRankings
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.Ranking);
    }
    
    public IEnumerable<UserRankingInfo> GetUserRankingInfo(int userId)
    {
        return DbContext.UserRankings
            .Where(ur => ur.UserId == userId)
            .Include(ur => ur.Ranking)
            .Select(ur => new UserRankingInfo
            {
                RankingId = ur.RankingId,
                Name = ur.Ranking.Name,
                Description = ur.Ranking.Description,
                Role = ur.UserRole
            });
    }

    public UserRole? GetUserRole(int userId, int rankingId)
    {
        return DbContext.UserRankings
            .Where(ur => ur.UserId == userId && ur.RankingId == rankingId)
            .Select(ur => ur.UserRole)
            .FirstOrDefault();
    }

    public Ranking? GetRankingWithData(int rankingId)
    {
        return DbContext.Rankings
            .Include(r => r.Alternatives)
            .Include(r => r.Criteria)
            .Include(r => r.Results)
            .FirstOrDefault(r => r.RankingId == rankingId);
    }

    [SuppressMessage("ReSharper.DPA", "DPA0007: Large number of DB records")]
    public Ranking? GetRankingWithAnswers(int rankingId)
    {
        return DbContext.Rankings
            .Where(r => r.RankingId == rankingId)
            .Include(r => r.Alternatives)
            .Include(r => r.Criteria)
            .Include(r => r.Answers)
            .Include(r => r.CriterionAnswers)
            .FirstOrDefault();
    } 

    public int AddRanking(Ranking ranking)
    {
        DbContext.Rankings.Add(ranking);
        DbContext.SaveChanges();
        return ranking.RankingId;
    }


    public Status AddUserRankingRole(int userId, int rankingId, UserRole userRole)
    {
        DbContext.UserRankings.Add(new UserRanking
        {
            UserId = userId,
            UserRole = userRole,
            RankingId = rankingId
        });
        return DbContext.SaveChanges() > 0 
            ? Status.Ok 
            : Status.DatabaseAddError;
    }

        
    public bool UserRankingExists(int userId, string name)
    {
        var query =
            from ranking in DbContext.Rankings
            join ur in DbContext.UserRankings on ranking.RankingId equals ur.RankingId
            where ur.UserId == userId && ranking.Name == name
            select ranking.RankingId;

        return query.Any();
    }

    public void ListUserRankings(int userId)
    {
        var user = DbContext.Users.FirstOrDefault(u => u.UserId == userId);
        Console.WriteLine($"All rankings of User {userId} ({user?.Username})");
        var ids = DbContext.UserRankings.Where(ur => ur.UserId == userId).Select(ur => ur.RankingId);
        foreach (var ranking in DbContext.Rankings.Where(r => ids.Contains(r.RankingId)))
        {
            Console.WriteLine($"{ranking.RankingId, 5} | {ranking.Name, 20} | {ranking.CreationDate, 20}");
        }
    }

    public List<UserRankingJsonBase> GetRankingUserRoles(int rankingId)
    {
        return DbContext.UserRankings
            .Where(ur => ur.RankingId == rankingId)
            .Include(ur => ur.User)
            .Select(ur => new UserRankingJsonBase
            {
                UserId = ur.UserId,
                Username = ur.User.Username,
                Role = ur.UserRole
            }).ToList();
    }
}