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
        [Authorize(Roles = "admin,user")]
        [HttpGet]
        [Route("InMannschaft/{MannschaftId}")]
        public IEnumerable<object> GetPersonenByMannschaftId(int MannschaftId)
        {
            return getList(Db.Personen.Where(x=> x.MannschaftId == MannschaftId).ToList());
        }

        // GET: api/Person/{PersonId}
        [Authorize(Roles = "admin,user")]
        [HttpGet("{PersonId}", Name = "Get")]
        public IActionResult GetPersonbyId(int PersonId)
        {
            IPerson person = Db.Personen.Where(x=> x.PersonId == PersonId).FirstOrDefault();
            if(person == null)
            {
                return NotFound();
            }
            else
            {
                if(person is Spieler)
                {
                    person = person as Spieler;
                }
                else if(person is Trainer)
                {
                    person = person as Trainer;
                }
                return Ok(person);
            }
        }
        // GET: api/Person
        [Authorize(Roles = "admin,user")]
        [HttpGet]
        public IEnumerable<object> GetAllPerson()
        {
            return getList(Db.Personen.ToList());
        }
        private IEnumerable<object> getList(List<Person> personen)
        {
            var lst = new List<object>();
            foreach (var item in personen)
            {
                if (item is Spieler)
                {
                    lst.Add(item as Spieler);
                }
                else if (item is Trainer)
                {
                    lst.Add(item as Trainer);
                }
            }
            return lst;
        }
        // POST: api/Person/Spieler
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
