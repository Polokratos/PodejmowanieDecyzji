namespace DecisionMakingServer;

public static class DbContextProvider
{
    public static readonly DecisionDbContext DbContext = new DecisionDbContext();
}