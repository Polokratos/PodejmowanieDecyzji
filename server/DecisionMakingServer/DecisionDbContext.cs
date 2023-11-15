using DecisionMakingServer.Models;
using Microsoft.EntityFrameworkCore;

namespace DecisionMakingServer;

public class DecisionDbContext : DbContext
{
    public DbSet<Alternative> Alternatives { get; set; }
    public DbSet<Answer> Answers { get; set; }
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
        
        var connectionString = configuration.GetConnectionString("cs1");
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // set double primary key in UserRankings
        modelBuilder.Entity<UserRanking>().HasKey(ur => new { UserID = ur.UserId, RankingID = ur.RankingId });

        // set NO cascade to Alternatives on delete of Answer
        modelBuilder.Entity<Answer>()
            .HasOne(a => a.LeftAlternative)
            .WithMany(alt => alt.LeftAnswers)
            .HasForeignKey(ans => ans.LeftAlternativeId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Answer>()
            .HasOne(a => a.RightAlternative)
            .WithMany(alt => alt.RightAnswers)
            .HasForeignKey(ans => ans.RightAlternativeId)
            .OnDelete(DeleteBehavior.NoAction);
        
        // set NO cascade to Alternatives and Criteria on delete of Result
        modelBuilder.Entity<Result>()
            .HasOne(r => r.Alternative)
            .WithMany(a => a.Results)
            .HasForeignKey(a => a.AlternativeId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Result>()
            .HasOne(r => r.Criterion)
            .WithMany(c => c.Results)
            .HasForeignKey(a => a.CriterionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}