#region Dateikopf
// Autor:       Maher Al Abbasi       
// Datum:      02.02.2021
#endregion

using Microsoft.AspNetCore.Mvc;
using Common;

namespace IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthenticateService _authenticateService;
        public AuthController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }
        [HttpPost]
        public IActionResult Post([FromBody] User model)
        {
            User user = _authenticateService.Authenticate(model.UserName, model.Password);
            if (user == null)
                return BadRequest(new { Message = "Falscher Benutzer oder Kennwort" });
            else
                return Ok(user);
        }
    }
}
