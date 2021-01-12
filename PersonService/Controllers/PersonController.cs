using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Common;
using PersonService.Models;

namespace PersonService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        public PersonDbContext Db { get; set; }

        public PersonController()
        {
            Db = new PersonDbContext();
        }

        // GET: api/Person
        //[Authorize(Policy = "person.read")]
        //[Authorize]
        [HttpGet]
        public IEnumerable<IPerson> GetAllPersonen()
        {
            return Db.Personen.ToList();
        }

        // GET: api/Person/5
        [HttpGet("{id}", Name = "Get")]
        public IEnumerable<IPerson> GetByMannschaftId(int id)
        {
            return Db.Personen.Where(x=> x.MannschaftId == id).ToList();
        }

        // POST: api/Person
        [HttpPost]
        [Route("Spieler")]
        public IActionResult AddSpieler([FromBody] Spieler spieler)
        {
            return Ok();
        }

        // POST: api/Person
        [HttpPost]
        [Route("Trainer")]
        public IActionResult AddTrainer([FromBody] Trainer trainer)
        {
            return Ok();
        }

        // PUT: api/Person/5
        [HttpPut]
        [Route("Spieler/{id}")]
        public IActionResult EditSpieler(int id, [FromBody] Spieler spieler)
        {
            return Ok();
        }
        [HttpPut]
        [Route("Trainer/{id}")]
        public IActionResult EditTrainer(int id, [FromBody] Trainer trainer)
        {
            return Ok();
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            Person person = null;
            if (person is Spieler)
            {
                return Ok("Spieler");
            }
            else if (person is Trainer)
            {
                return Ok("Trainer");
            }
            else return BadRequest();
        }
    }
}
