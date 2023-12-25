using System.Runtime.InteropServices;
using DecisionMakingServer.Models;
using Microsoft.EntityFrameworkCore;

namespace DecisionMakingServer;

public partial class DecisionDbContext : DbContext
{
    public DbSet<Alternative> Alternatives { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<CriterionAnswer> CriterionAnswers { get; set; }
    public DbSet<Criterion> Criteria { get; set; }
    public DbSet<Ranking> Rankings { get; set; }
    public DbSet<Result> Results { get; set; }
    public DbSet<Scale> Scales { get; set; }
    public DbSet<ScaleValue> ScaleValues { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRanking> UserRankings { get; set; }
    

    public DecisionDbContext()
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (optionsBuilder.IsConfigured) return;
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        
        if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) //FIXME: A proper configuration injection would be in order, but who cares. 
        {
            string? connectionString = configuration.GetConnectionString("cs2");
            optionsBuilder.UseMySql(connectionString,new MySqlServerVersion(new Version("8.0.35")));
        }
        else
        {
            string? connectionString = configuration.GetConnectionString("cs1");    
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SetupPivots(modelBuilder);
        SetupCascade(modelBuilder);
        SetupRankings(modelBuilder);
    }
}