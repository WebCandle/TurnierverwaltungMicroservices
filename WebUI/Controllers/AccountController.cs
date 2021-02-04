using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Common;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        private bool ValidateLogin(string userName, string password,out User user)
        {
            try
            {
                string URL = Startup.APIGatewayHost + "api-user/auth";
                var request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "application/json";
                request.Method = "POST";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    LoginModel model = new LoginModel(userName, password);
                    string json = JsonConvert.SerializeObject(model);
                    streamWriter.Write(json);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        user = JsonConvert.DeserializeObject<User>(result);
                        return true;
                    }
                    else
                    {
                        user = null;
                        return false;
                    }
                }
            }
            catch
            {
                user = null;
                return false;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ValidateLogin(userName, password,out User user))
            {
                var claims = new List<Claim>
                {
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("token", user.Token),
                    new Claim("role",user.Role)
                };

                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", user.Role, user.Role)));

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("/");
                }
            }
            ViewData["ErrorMessage"] = "Falscher Benutzer oder Kennwort";
            return View();
        }

        public IActionResult AccessDenied(string returnUrl = null)
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
