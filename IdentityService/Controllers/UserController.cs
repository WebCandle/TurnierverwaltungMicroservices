﻿#region Dateikopf
// Autor:       Maher Al Abbasi       
// Datum:      03.02.2021
#endregion

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserDbContext Db { get; set; }
        public UserController()
        {
            Db = new UserDbContext();
        }
        // GET: api/<UserController>
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return Db.Users.ToArray();
        }

        // GET api/<UserController>/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return Db.Users.Where(u=> u.UserId == id).FirstOrDefault();
        }

        // POST api/<UserController>
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Post([FromBody] User model)
        {
            if(model == null)
            {
                return BadRequest();
            }
            else
            {
                Db.Users.Add(model);
                int state = Db.SaveChanges();
                return Ok(state);
            }
        }

        // PUT api/<UserController>/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User model)
        {
            User user = Db.Users.Where(u => u.UserId == id).FirstOrDefault();
            if(user == null)
            {
                return NotFound();
            }
            else
            {
                user.Asign(model);
                int state = Db.SaveChanges();
                return Ok(state);
            }
        }

        // DELETE api/<UserController>/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User user = Db.Users.Where(u => u.UserId == id).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                Db.Users.Remove(user);
                int state = Db.SaveChanges();
                return Ok(state);
            }
        }
    }
}
