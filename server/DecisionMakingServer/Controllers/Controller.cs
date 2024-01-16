using DecisionMakingServer.APIModels;
using DecisionMakingServer.Enums;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DecisionMakingServer.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller : ControllerBase
{
    [EnableCors]
    [HttpPost, Route("login")]
    [ProducesResponseType(typeof(string), 200)]
    public IActionResult Login([FromBody] UserLoginDTO userLoginDto)
    {
        Console.WriteLine($"Received Login request: {userLoginDto.Username}, {userLoginDto.Password}");
        (string sessionToken, Status status) = RequestManager.Login(userLoginDto);
        return status == Status.Ok
            ? Ok(sessionToken) 
            : StatusCode(400, status.ToString());
    }

    
    [EnableCors]
    [HttpPost, Route("headers")]
    [ProducesResponseType(typeof(IEnumerable<RankingHeaderDTO>), 200)]
    public IActionResult Headers([FromBody] string sessionToken)
    {
        Console.WriteLine($"Received Headers request: st={sessionToken}");
        (var userRankings, Status status) = RequestManager.GetUserRankingIds(sessionToken);
        return status == Status.Ok
            ? Ok(userRankings) 
            : StatusCode(400, status.ToString());
    }

    
    [EnableCors]
    [HttpPost, Route("ranking/{rankingId:int}")]
    [ProducesResponseType(typeof(RankingDTO), 200)]
    public IActionResult GetRanking([FromBody] string sessionToken, int rankingId)
    {
        Console.WriteLine($"Received GetRanking request: st={sessionToken}, id={rankingId}");
        var (r, _) = RequestManager.GetRankingData(sessionToken, rankingId);
        Console.WriteLine($"Returning {r?.Name}");
        return Ok(r);
    }

    
    [EnableCors]
    [HttpPost, Route("create")]
    public IActionResult CreateRanking([FromBody] RankingDTO rankingDto)
    {
        Console.WriteLine($"Received Create request: st={rankingDto.SessionToken}");
        string sessionToken = rankingDto.SessionToken ?? "";
        Status s = RequestManager.CreateRanking(rankingDto, sessionToken);
        return s == Status.Ok
            ? Ok()
            : StatusCode(400, s);
    }

    
    [EnableCors]
    [HttpPost, Route("submit")]
    public IActionResult Submit([FromBody] RankingPostDTO rankingData)
    {
        Console.WriteLine($"Received Submit request: st={rankingData.SessionToken}");
        Status s = RequestManager.AddRankingAnswers(rankingData);
        return s == Status.Ok
            ? Ok("{}")
            : StatusCode(400, s);
    }


    [EnableCors]
    [HttpPost, Route("results/{rankingId:int}")]
    public IActionResult CalculateResults([FromBody] string sessionToken, int rankingId)
    {
        var (results, s) = RequestManager.GetRankingResults(sessionToken, rankingId);
        return s == Status.Ok
            ? Ok(results)
            : StatusCode(400, s);
    }


    [EnableCors]
    [HttpPost, Route("getjson/{rankingId:int}")]
    public IActionResult GetJson([FromBody] string sessionToken, int rankingId)
    {
        (string? json, var status) = RequestManager.GetJson(sessionToken, rankingId);
        return status == Status.Ok
            ? Ok(json)
            : StatusCode(400, status);
    }
}
