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

        // GET: api/Person/All/{MannschaftId}
        //[Authorize(Policy = "person.read")]
        //[Authorize]
        [HttpGet]
        [Route("InMannschaft/{MannschaftId}")]
        public IEnumerable<IPerson> GetAllPersonen(int MannschaftId)
        {
            return Db.Personen.Where(x=> x.MannschaftId == MannschaftId).ToList();
        }

        // GET: api/Person/{PersonId}
        [HttpGet("{PersonId}", Name = "Get")]
        public IActionResult GetPersonbyMannschaft(int PersonId)
        {
            IPerson person = Db.Personen.Where(x=> x.PersonId == PersonId).FirstOrDefault();
            if(person == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(person);
            }
        }
        // GET: api/Person
        [HttpGet]
        public IEnumerable<Person> GetPerson(int PersonId)
        {
            return Db.Personen.ToList();
        }

        // POST: api/Person/Spieler
        [HttpPost]
        [Route("Spieler")]
        public IActionResult AddSpieler([FromBody] Spieler spieler)
        {
            if(spieler == null)
            {
                return BadRequest();
            }
            else
            {
                Db.Personen.Add(spieler);
                int state = Db.SaveChanges();
                return Ok(state);
            }
        }

        // POST: api/Person/Trainer
        [HttpPost]
        [Route("Trainer")]
        public IActionResult AddTrainer([FromBody] Trainer trainer)
        {
            if (trainer == null)
            {
                return BadRequest();
            }
            else
            {
                Db.Personen.Add(trainer);
                int state = Db.SaveChanges();
                return Ok(state);
            }
        }

        // PUT: api/Person/Spieler/5
        [HttpPut]
        [Route("Spieler/{PersonId}")]
        public IActionResult EditSpieler(int PersonId, [FromBody] Spieler spieler)
        {
            if(spieler == null)
            {
                return BadRequest();
            }
            else
            {
                IPerson person = Db.Personen.Where(x => x.PersonId == PersonId).FirstOrDefault();
                if(person == null)
                {
                    return NotFound();
                }
                else
                {
                    person.Asign(spieler);
                    int state = Db.SaveChanges();
                    return Ok(state);
                }
            }
        }
        [HttpPut]
        [Route("Trainer/{PersonId}")]
        public IActionResult EditTrainer(int PersonId, [FromBody] Trainer trainer)
        {
            if (trainer == null)
            {
                return BadRequest();
            }
            else
            {
                IPerson person = Db.Personen.Where(x => x.PersonId == PersonId).FirstOrDefault();
                if (person == null)
                {
                    return NotFound();
                }
                else
                {
                    person.Asign(trainer);
                    int state = Db.SaveChanges();
                    return Ok(state);
                }
            }
        }

        // DELETE: api/Person/5
        [HttpDelete("{PersonId}")]
        public IActionResult DeletePerson(int PersonId)
        {
            IPerson person = Db.Personen.Where(x => x.PersonId == PersonId).FirstOrDefault();
            if (person == null)
            {
                return NotFound();
            }
            else
            {
                Db.Personen.Remove((Person)person);
                int state = Db.SaveChanges();
                return Ok(state);
            }
        }
    }
}
