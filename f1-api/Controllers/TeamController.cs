using f1_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace f1_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ILogger<TeamController> _logger;
        private readonly List<Team> _teams;

        public TeamController(ILogger<TeamController> logger)
        {
            _logger = logger;
            _teams = new List<Team>
            {
                new Team
                {
                    Id = 1,
                    Manufacturer = "Team1",
                    Driver1 = new Driver { Id = 1, Name = "Driver1_Team1", Age = 25, Nationality = "Country1" },
                    Driver2 = new Driver { Id = 2, Name = "Driver2_Team1", Age = 28, Nationality = "Country2" }
                },
                new Team
                {
                    Id = 2,
                    Manufacturer = "Team2",
                    Driver1 = new Driver { Id = 3, Name = "Driver1_Team2", Age = 23, Nationality = "Country3" },
                    Driver2 = new Driver { Id = 4, Name = "Driver2_Team2", Age = 26, Nationality = "Country4" }
                }
            };
        }

        [HttpGet(Name = "GetAllTeams")]
        public IEnumerable<Team> Get()
        {
            return _teams;
        }

        [HttpGet("{id}", Name = "GetTeamById")]
        public IActionResult GetById(int id)
        {
            var team = _teams.FirstOrDefault(t => t.Id == id);

            if (team == null)
                return NotFound();

            return Ok(team);
        }

        [HttpPost(Name = "CreateTeam")]
        public IActionResult Create(Team team)
        {
            team.Id = _teams.Max(t => t.Id) + 1;

            _teams.Add(team);

            return CreatedAtRoute("GetTeamById", new { id = team.Id }, team);
        }

        [HttpPut("{id}", Name = "UpdateTeam")]
        public IActionResult Update(int id, Team updatedTeam)
        {
            var existingTeam = _teams.FirstOrDefault(t => t.Id == id);

            if (existingTeam == null)
                return NotFound();

            existingTeam.Manufacturer = updatedTeam.Manufacturer;
            existingTeam.Driver1 = updatedTeam.Driver1;
            existingTeam.Driver2 = updatedTeam.Driver2;

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteTeam")]
        public IActionResult Delete(int id)
        {
            var team = _teams.FirstOrDefault(t => t.Id == id);

            if (team == null)
                return NotFound();

            _teams.Remove(team);

            return NoContent();
        }
    }
}
