using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AuthService.Models;
namespace AuthService.Controllers
{
    public class LoginController : ApiController
    {
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public LoginModel Get(int id)
        {
            return new LoginModel("user","user");
        }

        // POST: api/Login
        [HttpPost]
        [Route("api/Login")]
        public IHttpActionResult Post([FromBody] LoginModel model)
        {
            if(model.UserName == "user" && model.Password == "user")
            {
                model.ID = 5;
                model.Rolle = "USER";
                return Ok(model);
            }
            else
            {
                return Unauthorized();
            }
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
