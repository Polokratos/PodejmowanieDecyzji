using Microsoft.EntityFrameworkCore;
using DecisionMakingServer.Enums;
using DecisionMakingServer.Models;

namespace DecisionMakingServer.Repositories;

public class RankingRepository : AbstractDbRepository
{
    public IEnumerable<Ranking> GetUserRankings(int userId)
    {
        return DbContext.UserRankings
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.Ranking);
    }

    public Ranking? GetRankingWithData(int rankingId)
    {
        return DbContext.Rankings
            .Include(r => r.Alternatives)
            .Include(r => r.Criteria)
            .Include(r => r.Results)
            .FirstOrDefault(r => r.RankingId == rankingId);
    }

    public Ranking? GetRankingWithAnswers(int rankingId)
    {
        return DbContext.Rankings
            .Include(r => r.Alternatives)
            .Include(r => r.Criteria)
            .Include(r => r.Answers)
            .Include(r => r.CriterionAnswers)
            .FirstOrDefault(r => r.RankingId == rankingId);
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
}