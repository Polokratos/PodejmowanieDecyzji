using DecisionMakingServer.APIModels;
using DecisionMakingServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace DecisionMakingServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase 
    { 

        private readonly ILogger<Controller> _logger;

        public Controller(ILogger<Controller> logger)
        {
            _logger = logger;
        }


        [HttpPost, Route("login")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Login([FromBody] UserLoginDTO userLoginDto)
        {
            if (new Random().Next() % 3 != 0)
                return Ok("ANiceSessionTokenHehe");
            return StatusCode(400, "Invalid login data");
        }

        
        [HttpPost, Route("headers")]
        [ProducesResponseType(typeof(IEnumerable<RankingHeaderDTO>), 200)]
        public IActionResult Headers([FromBody] string sessionToken)
        {
            var headers = new[]
            {
                new RankingHeaderDTO { Id = 0, Name = "A test ranking", Description = "lol"},
                new RankingHeaderDTO { Id = 2, Name = "A test ranking #2", Description = "foo"},
                new RankingHeaderDTO { Id = 63, Name = "A test ranking #3", Description = "6nt"}
            };

            return Ok(headers);
        }

        
        [HttpPost, Route("survey/{surveyId:int}")]
        [ProducesResponseType(typeof(RankingDTO), 200)]
        public IActionResult Survey([FromBody] string sessionToken, int surveyId)
        {
            var r =  new RankingDTO
            {
                RankingId = surveyId,
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

        
        [HttpPost, Route("create")]
        public IActionResult CreateRanking([FromBody] RankingDTO rankingDto)
        {
            return Ok();
        }

        
        [HttpPost, Route("submit")]
        public IActionResult Submit([FromBody] RankingPostDTO rankingData)
        {
            return Ok();
        }
    }
}