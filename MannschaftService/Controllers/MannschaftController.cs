#region Dateikopf
// Autor:       Maher Al Abbasi       
// Datum:      03.02.2021
#endregion

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Common;
using MannschaftService.Models;
using Microsoft.AspNetCore.Authorization;

namespace MannschaftService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MannschaftController : ControllerBase
    {
        public MannschaftDbContext Db { get; set; }
        public MannschaftController()
        {
            Db = new MannschaftDbContext();
        }
        // GET: api/<MannschaftController>
        [Authorize(Roles = "admin,user")]
        [HttpGet]
        public IEnumerable<Mannschaft> Get()
        {
            return Db.Mannschaften.ToList();
        }

        // GET api/<MannschaftController>/5
        [Authorize(Roles = "admin,user")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Mannschaft mannschaft = Db.Mannschaften.Where(x => x.MannschaftId == id).FirstOrDefault();
            if(mannschaft == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mannschaft);
            }
        }

        // POST api/<MannschaftController>
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Post([FromBody] Mannschaft mannschaft)
        {
            if(mannschaft == null)
            {
                return BadRequest();
            }
            else
            {
                Db.Mannschaften.Add(mannschaft);
                int state = Db.SaveChanges();
                return Ok(state);
            }
        }

        // PUT api/<MannschaftController>/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Mannschaft neu_mannschaft)
        {
            if(neu_mannschaft == null)
            {
                return BadRequest();
            }
            else
            {
                Mannschaft mannschaft = Db.Mannschaften.Where(x => x.MannschaftId == id).FirstOrDefault();
                if(mannschaft==null)
                {
                    return NotFound();
                }
                else
                {
                    mannschaft.Asign(neu_mannschaft);
                    int state = Db.SaveChanges();
                    return Ok(state);
                }
            }
        }

        // DELETE api/<MannschaftController>/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Mannschaft mannschaft = Db.Mannschaften.Where(x => x.MannschaftId == id).FirstOrDefault();
            if(mannschaft == null)
            {
                return NotFound();
            }
            else
            {
                Db.Mannschaften.Remove(mannschaft);
                int state = Db.SaveChanges();
                return Ok(state);
            }
        }
    }
}
