namespace DecisionMakingServer.Repositories;

public class AbstractDbRepository
{
    protected readonly DecisionDbContext DbContext;

    protected AbstractDbRepository()
    {
        DbContext = new DecisionDbContext();
    }

    protected int SaveChanges()
    {
        return DbContext.SaveChanges();
    }
}