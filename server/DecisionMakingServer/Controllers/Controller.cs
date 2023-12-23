using DecisionMakingServer.APIModels;
using DecisionMakingServer.Enums;
using DecisionMakingServer.Models;
using DecisionMakingServer.Repositories;
using DecisionMakingServer.Session;
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
            var r =  new RankingDTO
            {
                RankingId = rankingId,
                Name = "A test ranking",
                Description = "lol",
                AggregationMethod = AggregationMethod.Default,
                Alternatives = new List<AlternativeDTO>
                {
                    new() { AlternativeId = 0, Name = "Alt1", Description = "Alt1d" },
                    new() { AlternativeId = 1, Name = "Alt2", Description = "Alt2d" },
                    new() { AlternativeId = 5, Name = "Alt3", Description = "Alt3d" },
                    new() { AlternativeId = 42, Name = "Alt4", Description = "Alt4d" },
                    new() { AlternativeId = 69, Name = "Alt5", Description = "Alt5d" },
                },
                CreationDate = DateTime.Now.Subtract(TimeSpan.FromHours(2)),
                AskOrder = "123",
                CalculationMethod = CalculationMethod.GMM,
                Criteria = new List<CriterionDTO>
                {
                    new() { CriterionId = 0, Name = "Crit1", Description = "Crit1d" },
                    new() { CriterionId = 13, Name = "Crit2", Description = "Crit2d" }
                },
                EndDate = DateTime.Now.Add(TimeSpan.FromDays(10)),
                IsComplete = false,
                Scale = new List<ScaleValueDTO> {
                    new() {Description = "bad", Value = 3},
                    new() {Description = "good", Value = 6}
                },
                Results = null
            };

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
            return Ok("Not implemented");
        }
    }
}