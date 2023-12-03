namespace DecisionMakingServer.Repositories;

public class AbstractDbRepository
{
    protected readonly DecisionDbContext DbContext;

    protected AbstractDbRepository()
    {
        DbContext = DbContextProvider.DbContext;
    }

    protected int SaveChanges()
    {
        return DbContext.SaveChanges();
    }
}