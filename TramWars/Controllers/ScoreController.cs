using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TramWars.DTO;
using TramWars.Persistence.Repositories.Interfaces;

namespace TramWars.Controllers
{
    public class ScoreController : Controller
    {
        private readonly IUserRepository userRepository;

        public ScoreController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [EnableCors("CorsPolicy")]
        [Route("scores/highest")]
        [HttpGet]
        public ActionResult GetHighScores()
        {
            var highestScoringUsers = userRepository.GetByTopScores(10);
            var dtos = highestScoringUsers.Select(x => new HighScoreDTO 
            {
                UserName = x.UserName,
                Score = x.Score
            });
            return Ok(dtos);
        }
    }
}