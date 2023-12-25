using DecisionMakingServer.Models;
using Microsoft.EntityFrameworkCore;

namespace DecisionMakingServer;

public partial class DecisionDbContext
{
    private void SetupPivots(ModelBuilder modelBuilder)
    {
        // set double primary key in UserRankings
        modelBuilder.Entity<UserRanking>().HasKey(ur => new { UserID = ur.UserId, RankingID = ur.RankingId });
    }
    
    private void SetupCascade(ModelBuilder modelBuilder) 
    {
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
        
        // set NO cascade to Answers on delete of Criterion
        modelBuilder.Entity<Answer>()
            .HasOne(a => a.Criterion)
            .WithMany(c => c.Answers)
            .HasForeignKey(a => a.CriterionId)
            .OnDelete(DeleteBehavior.NoAction);
    }


    private void SetupRankings(ModelBuilder modelBuilder)
    {
        // setup 1-to-1 ranking-scale
        modelBuilder.Entity<Ranking>()
            .HasOne(r => r.Scale)
            .WithOne(s => s.Ranking)
            .HasForeignKey<Ranking>(r => r.ScaleId)
            .IsRequired(false);

        
        // Foreign keys
        modelBuilder.Entity<Criterion>()
            .HasOne(c => c.Ranking)
            .WithMany(r => r.Criteria)
            .HasForeignKey(c => c.RankingId);

        modelBuilder.Entity<Alternative>()
            .HasOne(a => a.Ranking)
            .WithMany(r => r.Alternatives)
            .HasForeignKey(a => a.RankingId);

        modelBuilder.Entity<UserRanking>()
            .HasOne(ur => ur.Ranking)
            .WithMany(r => r.UserRankings)
            .HasForeignKey(ur => ur.RankingId);

        modelBuilder.Entity<Result>()
            .HasOne(r => r.Ranking)
            .WithMany(r => r.Results)
            .HasForeignKey(r => r.RankingId);

        modelBuilder.Entity<Answer>()
            .HasOne(a => a.Ranking)
            .WithMany(r => r.Answers)
            .HasForeignKey(a => a.RankingId);

        modelBuilder.Entity<CriterionAnswer>()
            .HasOne(a => a.Ranking)
            .WithMany(r => r.CriterionAnswers)
            .HasForeignKey(a => a.RankingId);
    }


    private void SetupAnswers(ModelBuilder modelBuilder)
    {
        
    }
}