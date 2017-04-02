using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TramWars.Domain;
using TramWars.Dto;

namespace TramWars.Controllers
{
    public class ScoreController : Controller
    {
        private readonly IUsersFacade _users;

        public ScoreController(IUsersFacade users)
        {
            _users = users;
        }

        [EnableCors("CorsPolicy")]
        [Route("scores/highest")]
        [HttpGet]
        public async Task<ActionResult> GetHighScores()
        {
            var highestScoringUsers = await _users.GetByTopScoresAsync(10);
            var dtos = highestScoringUsers.Select(x => new HighScoreDto
            {
                UserName = x.UserName,
                Score = x.Score
            });
            return Ok(dtos);
        }
    }
}