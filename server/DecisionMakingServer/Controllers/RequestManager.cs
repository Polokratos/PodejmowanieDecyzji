using DecisionMakingServer.APIModels;
using DecisionMakingServer.Calculation;
using DecisionMakingServer.Enums;
using DecisionMakingServer.Models;
using DecisionMakingServer.Models.NonDbModels;
using DecisionMakingServer.Repositories;
using DecisionMakingServer.Session;
using Newtonsoft.Json;

namespace DecisionMakingServer.Controllers;

public static class RequestManager
{
    private static readonly SessionManager SessionManager = new();
    
    public static (string, Status) Login(UserLoginDTO dto)
    {
        return SessionManager.Login(dto.Username, dto.Password);
    }

    public static (IEnumerable<RankingHeaderDTO>, Status) GetUserRankingIds(string sessionToken)
    {
        int userId = SessionManager.GetUserId(sessionToken);
        if (userId == -1)
            return (new List<RankingHeaderDTO>(), Status.InvalidSession);

        var repo = new RankingRepository();
        var rankings = repo.GetUserRankingInfo(userId)
            .Select(ur => ur.ToRankingHeaderDto());

        return (rankings, Status.Ok);
    }

    public static Status CreateRanking(RankingDTO dto, string sessionToken)
    {
        int userId = SessionManager.GetUserId(sessionToken);
        if (userId == -1)
            return Status.InvalidSession;

        var rankingRepo = new RankingRepository();
        if (rankingRepo.UserRankingExists(userId, dto.Name))
            return Status.AlreadyExistsInDb;
        
        Ranking ranking = dto.ToRanking();
        
        ranking.UserRankings.Add(new UserRanking
        {
            UserId = SessionManager.GetUserId(sessionToken),
            UserRole = UserRole.Owner
        });

        var userRepo = new UserRepository();
        var invitedUsers = userRepo.GetUsersByNames(dto.InvitedUsers);
        ranking.UserRankings.AddRange(
            invitedUsers.Select(u => new UserRanking
            {
                UserId = u,
                UserRole = UserRole.Assignee
            }));

        return rankingRepo.AddRanking(ranking) > 0
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

        var answerRepo = new AnswerRepository();
        return answerRepo.AddAnswers(answers);
    }


    public static (RankingDTO?, Status) GetRankingData(string sessionToken, int rankingId)
    {
        int userId = SessionManager.GetUserId(sessionToken);
        if (userId == -1)
            return (null, Status.InvalidSession);

        var rankingRepo = new RankingRepository();
        Ranking? ranking = rankingRepo.GetRankingWithData(rankingId);
        return ranking == null 
            ? (null, Status.DatabaseGetError) 
            : (ranking.ToDto(), Status.Ok);
    }


    public static (List<Result>?, Status) CalculateResults(int rankingId)
    {
        var rankingRepo = new RankingRepository();
        var ranking = rankingRepo.GetRankingWithAnswers(rankingId);
        if (ranking == null) 
            return (null, Status.DatabaseGetError);

        var resultRepo = new ResultRepository();
        resultRepo.ClearResults(rankingId);

        var calculator = new PriorityMeanRankingCalculator(ranking);
        var results = calculator.Calculate().ToList();
        
        resultRepo.AddResults(results);

        return (results, Status.Ok);
    }


    public static (List<ResultDTO>?, Status) GetRankingResults(string sessionToken, int rankingId)
    {
        int userId = SessionManager.GetUserId(sessionToken);
        if (userId == -1)
            return (null, Status.InvalidSession);

        var rankingRepo = new RankingRepository();
        if (rankingRepo.GetUserRole(userId, rankingId) != UserRole.Owner)
            return (null, Status.Forbidden);

        var (resultsRaw, status) = CalculateResults(rankingId);
        return (resultsRaw?.Select(r => r.ToDto()).ToList(), status);
    }


    public static (string?, Status) GetJson(string sessionToken, int rankingId)
    {
        int userId = SessionManager.GetUserId(sessionToken);
        if (userId == -1)
            return (null, Status.InvalidSession);

        var rankingRepo = new RankingRepository();
        if (rankingRepo.GetUserRole(userId, rankingId) != UserRole.Owner)
            return (null, Status.Forbidden);
        
        var ranking = rankingRepo.GetRankingWithAnswers(rankingId);
        if (ranking == null) 
            return (null, Status.DatabaseGetError);

        var resultRepo = new ResultRepository();
        ranking.Results = resultRepo.GetResults(rankingId).ToList();
        var users = rankingRepo.GetRankingUserRoles(rankingId);
        
        var jsonBase = ranking.ToJsonBase(users);

        string json = JsonConvert.SerializeObject(
            jsonBase, 
            Formatting.Indented,
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
        return (json, Status.Ok);
    }
}