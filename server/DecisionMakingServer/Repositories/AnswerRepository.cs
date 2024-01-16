using DecisionMakingServer.Enums;
using DecisionMakingServer.Models;

namespace DecisionMakingServer.Repositories;

public class AnswerRepository : AbstractDbRepository
{
    public Status AddAnswers(IEnumerable<Answer> answers)
    {
        var nonZeroAnswers = answers.Where(a => a.Value > 0);
        DbContext.Answers.AddRange(nonZeroAnswers);
        return DbContext.SaveChanges() > 0
            ? Status.Ok
            : Status.DatabaseAddError;
    }
}