using f1_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace f1_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RaceController : ControllerBase
    {
        private readonly ILogger<RaceController> _logger;
        private readonly List<Race> _races;

        public RaceController(ILogger<RaceController> logger)
        {
            _logger = logger;
            _races = new List<Race>
            {
                new Race { Id = 1, WinnerName = "Winner1", WinnerTime = new TimeOnly(1, 30), GrandPrix = "Grand Prix 1", NumberOfLaps = 20 },
                new Race { Id = 2, WinnerName = "Winner2", WinnerTime = new TimeOnly(1, 25), GrandPrix = "Grand Prix 2", NumberOfLaps = 25 }
            };
        }

        [HttpGet(Name = "GetAllRaces")]
        public IEnumerable<Race> Get()
        {
            return _races;
        }

        [HttpGet("{id}", Name = "GetRaceById")]
        public IActionResult GetById(int id)
        {
            var race = _races.FirstOrDefault(r => r.Id == id);

            if (race == null)
                return NotFound();

            return Ok(race);
        }

        [HttpPost(Name = "CreateRace")]
        public IActionResult Create(Race race)
        {
            race.Id = _races.Max(r => r.Id) + 1;

            _races.Add(race);

            return CreatedAtRoute("GetRaceById", new { id = race.Id }, race);
        }

        [HttpPut("{id}", Name = "UpdateRace")]
        public IActionResult Update(int id, Race updatedRace)
        {
            var existingRace = _races.FirstOrDefault(r => r.Id == id);

            if (existingRace == null)
                return NotFound();

            existingRace.WinnerName = updatedRace.WinnerName;
            existingRace.WinnerTime = updatedRace.WinnerTime;
            existingRace.GrandPrix = updatedRace.GrandPrix;
            existingRace.NumberOfLaps = updatedRace.NumberOfLaps;

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteRace")]
        public IActionResult Delete(int id)
        {
            var race = _races.FirstOrDefault(r => r.Id == id);

            if (race == null)
                return NotFound();

            _races.Remove(race);

            return NoContent();
        }
    }
}
