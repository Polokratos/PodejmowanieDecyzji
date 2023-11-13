using DecisionMakingServer.Models;
using Microsoft.EntityFrameworkCore;

namespace DecisionMakingServer;

public class DecisionDbContext : DbContext
{
    public DbSet<Poll> Polls { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserPoll> UserPolls { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<Alternative> Alternatives { get; set; } = null!;
    public DbSet<UserAlternative> UserAlternatives { get; set; } = null!;

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
        modelBuilder.Entity<UserPoll>().HasKey(up => new { up.UserId, up.PollId });
        modelBuilder.Entity<UserAlternative>().HasKey(ua => new { ua.UserId, ua.AlternativeId });
    }
}