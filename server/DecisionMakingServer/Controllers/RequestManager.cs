using DecisionMakingServer.APIModels;
using DecisionMakingServer.Calculation;
using DecisionMakingServer.Enums;
using DecisionMakingServer.Models;
using DecisionMakingServer.Repositories;
using DecisionMakingServer.Session;

namespace DecisionMakingServer.Controllers;

public static class RequestManager
{
    private static readonly SessionManager _sessionManager = new();
    private static readonly RankingRepository _rankingRepository = new();
    private static readonly AnswerRepository _answerRepository = new();
    
    public static (string, Status) Login(UserLoginDTO dto)
    {
        return _sessionManager.Login(dto.Username, dto.Password);
    }

    public static (IEnumerable<RankingHeaderDTO>, Status) GetUserRankings(string sessionToken)
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

    public static Status CreateRanking(RankingDTO dto, string sessionToken)
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

        return _rankingRepository.AddRanking(ranking) > 0
            ? Status.Ok
            : Status.DatabaseAddError;
    }


    public static Status AddRankingAnswers(RankingPostDTO dto)
    {
        int userId = _sessionManager.GetUserId(dto.SessionToken);
        if (userId == -1)
            return Status.InvalidSession;

        int rankingId = dto.RankingId;
        var answers = dto.Answers
            .Select(a => a.ToAnswer(userId))
            .ToList();
        
        answers.ForEach(a =>
        {
            a.RankingId = rankingId;
            a.UserId = userId;
        });
        
        return _answerRepository.AddAnswers(answers);
    }


    public static (RankingDTO?, Status) GetRankingData(string sessionToken, int rankingId)
    {
        int userId = _sessionManager.GetUserId(sessionToken);
        if (userId == -1)
            return (null, Status.InvalidSession);

        Ranking? ranking = _rankingRepository.GetRankingWithData(rankingId);
        return ranking == null 
            ? (null, Status.DatabaseGetError) 
            : (ranking.ToDto(), Status.Ok);
    }


    public static (List<ResultDTO>?, Status) GetRankingResults(string sessionToken, int rankingId)
    {
        int userId = _sessionManager.GetUserId(sessionToken);
        if (userId == -1)
            return (null, Status.InvalidSession);

        var ranking = _rankingRepository.GetRankingWithAnswers(rankingId);
        if (ranking == null) 
            return (null, Status.DatabaseGetError);

        var calculator = new RankingCalculator(ranking);
        var results = calculator.Calculate();

        return (results.Select(r => r.ToDto()).ToList(), Status.Ok);
    }
    

    public static int GetUserId(string sessionToken)
    {
        return _sessionManager.GetUserId(sessionToken);
    }
}