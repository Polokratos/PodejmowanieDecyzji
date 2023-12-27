using DecisionMakingServer.APIModels;
using DecisionMakingServer.Calculation;
using DecisionMakingServer.Enums;
using DecisionMakingServer.Models;
using DecisionMakingServer.Models.NonDbModels;
using DecisionMakingServer.Repositories;
using DecisionMakingServer.Session;

namespace DecisionMakingServer.Controllers;

public static class RequestManager
{
    private static readonly SessionManager SessionManager = new();
    private static readonly RankingRepository RankingRepository = new();
    private static readonly AnswerRepository AnswerRepository = new();
    private static readonly UserRepository UserRepository = new();
    
    public static (string, Status) Login(UserLoginDTO dto)
    {
        return SessionManager.Login(dto.Username, dto.Password);
    }

    public static (IEnumerable<RankingHeaderDTO>, Status) GetUserRankingIds(string sessionToken)
    {
        int userId = SessionManager.GetUserId(sessionToken);
        if (userId == -1)
            return (new List<RankingHeaderDTO>(), Status.InvalidSession);

        var rankings = RankingRepository.GetUserRankingInfo(userId)
            .Select(ur => ur.ToRankingHeaderDto());

        return (rankings, Status.Ok);
    }

    public static Status CreateRanking(RankingDTO dto, string sessionToken)
    {
        int userId = SessionManager.GetUserId(sessionToken);
        if (userId == -1)
            return Status.InvalidSession;
        if (RankingRepository.UserRankingExists(userId, dto.Name))
            return Status.AlreadyExistsInDb;
        
        Ranking ranking = dto.ToRanking();
        
        ranking.UserRankings.Add(new UserRanking
        {
            UserId = SessionManager.GetUserId(sessionToken),
            UserRole = UserRole.Owner
        });

        var invitedUsers = UserRepository.GetUsersByNames(dto.InvitedUsers);
        ranking.UserRankings.AddRange(
            invitedUsers.Select(u => new UserRanking
            {
                UserId = u,
                UserRole = UserRole.Assignee
            }));

        return RankingRepository.AddRanking(ranking) > 0
            ? Status.Ok
            : Status.DatabaseAddError;
    }


    public static Status AddRankingAnswers(RankingPostDTO dto)
    {
        int userId = SessionManager.GetUserId(dto.SessionToken);
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
        
        return AnswerRepository.AddAnswers(answers);
    }


    public static (RankingDTO?, Status) GetRankingData(string sessionToken, int rankingId)
    {
        int userId = SessionManager.GetUserId(sessionToken);
        if (userId == -1)
            return (null, Status.InvalidSession);

        Ranking? ranking = RankingRepository.GetRankingWithData(rankingId);
        return ranking == null 
            ? (null, Status.DatabaseGetError) 
            : (ranking.ToDto(), Status.Ok);
    }


    public static (List<ResultDTO>?, Status) GetRankingResults(string sessionToken, int rankingId)
    {
        int userId = SessionManager.GetUserId(sessionToken);
        if (userId == -1)
            return (null, Status.InvalidSession);

        var ranking = RankingRepository.GetRankingWithAnswers(rankingId);
        if (ranking == null) 
            return (null, Status.DatabaseGetError);

        var calculator = new JudgementMeanRankingCalculator(ranking);
        var results = calculator.Calculate();

        return (results.Select(r => r.ToDto()).ToList(), Status.Ok);
    }
    

    public static int GetUserId(string sessionToken)
    {
        return SessionManager.GetUserId(sessionToken);
    }
}