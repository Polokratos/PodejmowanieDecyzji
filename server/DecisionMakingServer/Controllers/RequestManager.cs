using DecisionMakingServer.APIModels;
using DecisionMakingServer.Enums;
using DecisionMakingServer.Models;
using DecisionMakingServer.Repositories;
using DecisionMakingServer.Session;

namespace DecisionMakingServer.Controllers;

public class RequestManager
{
    private readonly SessionManager _sessionManager = new();
    private readonly RankingRepository _rankingRepository = new();
    private readonly AnswerRepository _answerRepository = new();
    
    public (string, Status) Login(UserLoginDTO dto)
    {
        return _sessionManager.Login(dto.Username, dto.Password);
    }

    public (IEnumerable<RankingHeaderDTO>, Status) GetUserRankings(string sessionToken)
    {
        int userId = _sessionManager.GetUserId(sessionToken);
        if (userId == -1)
            return (new List<RankingHeaderDTO>(), Status.InvalidSession);

        var rankings = _rankingRepository.GetUserRankings(userId)
            .Select(r => new RankingHeaderDTO
            {
                Id = r.RankingId,
                Name = r.Name,
                Description = r.Description
            });

        return (rankings, Status.Ok);
    }

    public Status CreateRanking(RankingDTO dto, string sessionToken)
    {
        int userId = _sessionManager.GetUserId(sessionToken);
        if (userId == -1)
            return Status.InvalidSession;
        if (_rankingRepository.UserRankingExists(userId, dto.Name))
            return Status.AlreadyExistsInDb;
        
        Ranking ranking = dto.ToRanking();
        ranking.UserRankings.Add(new UserRanking
        {
            UserId = _sessionManager.GetUserId(sessionToken),
            UserRole = UserRole.Owner
        });

        return _rankingRepository.AddRanking(ranking);
    }


    public Status AddRankingResults(RankingPostDTO dto, string sessionToken)
    {
        int userId = _sessionManager.GetUserId(sessionToken);
        if (userId == -1)
            return Status.InvalidSession;

        int rankingId = dto.RankingId;
        var answers = dto.Answers
            .Select(a => a.ToAnswer())
            .ToList();
        
        answers.ForEach(a =>
        {
            a.RankingId = rankingId;
            a.UserId = userId;
        });
        
        return _answerRepository.AddAnswers(answers);
    }
    

    public int GetUserId(string sessionToken)
    {
        return _sessionManager.GetUserId(sessionToken);
    }
}