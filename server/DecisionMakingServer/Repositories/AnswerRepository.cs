using DecisionMakingServer.Enums;
using DecisionMakingServer.Models;

namespace DecisionMakingServer.Repositories;

public class AnswerRepository : AbstractDbRepository
{
    public Status AddAnswers(IEnumerable<Answer> answers)
    {
        DbContext.Answers.AddRange(answers);
        return DbContext.SaveChanges() > 0
            ? Status.Ok
            : Status.DatabaseAddError;
    }
}