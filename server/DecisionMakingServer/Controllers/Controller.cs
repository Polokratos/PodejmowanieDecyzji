using DecisionMakingServer.APIModels;
using DecisionMakingServer.Enums;
using DecisionMakingServer.Models;
using DecisionMakingServer.Repositories;
using DecisionMakingServer.Session;
using DecisionMakingServer.Tests;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace DecisionMakingServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        private readonly RequestManager _requestManager = new();

        [EnableCors]
        [HttpPost, Route("login")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Login([FromBody] UserLoginDTO userLoginDto)
        {
            if (userLoginDto is { Username: "aaa", Password: "bbb" })
                return Ok(Guid.NewGuid().ToString());
            
            Console.WriteLine($"Received Login request: {userLoginDto.Username}, {userLoginDto.Password}");
            (string sessionToken, Status status) = _requestManager.Login(userLoginDto);
            return status == Status.Ok
                ? Ok(sessionToken) 
                : StatusCode(400, status.ToString());
        }

        
        [EnableCors]
        [HttpPost, Route("headers")]
        [ProducesResponseType(typeof(IEnumerable<RankingHeaderDTO>), 200)]
        public IActionResult Headers([FromBody] string sessionToken)
        {
            (var userRankings, Status status) = _requestManager.GetUserRankings(sessionToken);
            return status == Status.Ok
                ? Ok(userRankings) 
                : StatusCode(400, status.ToString());
        }

        
        [EnableCors]
        [HttpPost, Route("ranking/{rankingId:int}")]
        [ProducesResponseType(typeof(RankingDTO), 200)]
        public IActionResult GetRanking([FromBody] string sessionToken, int rankingId)
        {
            if (rankingId == -1)
                return Ok(DummyData.RankingDto);

            var r = _requestManager.GetRankingData(sessionToken, rankingId);
            return Ok(r);
        }

        
        [EnableCors]
        [HttpPost, Route("create")]
        public IActionResult CreateRanking([FromBody] RankingDTO rankingDto)
        {
            string sessionToken = rankingDto.SessionToken;
            Status s = _requestManager.CreateRanking(rankingDto, sessionToken);
            return s == Status.Ok
                ? Ok()
                : StatusCode(400, s);
        }

        
        [EnableCors]
        [HttpPost, Route("submit")]
        public IActionResult Submit([FromBody] RankingPostDTO rankingData)
        {
            Status s = _requestManager.AddRankingAnswers(rankingData);
            return s == Status.Ok
                ? Ok()
                : StatusCode(400, s);
        }
    }
}