using DecisionMakingServer.Models;

namespace DecisionMakingServer.Repositories;

public class ResultRepository : AbstractDbRepository
{
    public void ClearResults(int rankingId)
    {
        var toRemove = DbContext.Results.Where(r => r.RankingId == rankingId);
        DbContext.Results.RemoveRange(toRemove);
        DbContext.SaveChanges();
    }

    public IEnumerable<Result> GetResults(int rankingId)
    {
        return DbContext.Results.Where(r => r.RankingId == rankingId);
    } 
}