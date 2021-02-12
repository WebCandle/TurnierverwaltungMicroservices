#region Dateikopf
// Autor:       Maher Al Abbasi       
// Datum:      09.02.2021
#endregion

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace WebUI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IHttpContextAccessor httpContextAccessor;
        public IList<User> MyUsers;
        public string LoadUserResponse;
        public UserController(IHttpContextAccessor _httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;
            loadUsers();
        }
        // GET: UserController
        public ActionResult Index()
        {
            ViewData["status"] = LoadUserResponse;
            return View(MyUsers);
        }
        private void loadUsers()
        {
            try
            {
                MyUsers = new List<User>();
                string URL = Startup.APIGatewayHost + "api-user";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "GET";
                request.Headers["Authorization"] = "Bearer " + httpContextAccessor.HttpContext.User.FindFirst("token").Value;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    MyUsers = JsonConvert.DeserializeObject<List<User>>(reader.ReadToEnd(), new PersonConverter());
                }
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    LoadUserResponse = response.StatusDescription;
                }
            }
            catch (Exception ex)
            {
                LoadUserResponse = ex.Message;
            }

        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            User user = new Common.User(collection["UserName"], collection["Password"],collection["Role"]);
            try
            {
                string URL = Startup.APIGatewayHost + "api-user";
                var request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "application/json";
                request.Method = "POST";
                request.Headers["Authorization"] = "Bearer " + httpContextAccessor.HttpContext.User.FindFirst("token").Value;

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(user);
                    streamWriter.Write(json);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewData["status"] = response.StatusDescription;
                        return View(user);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["status"] = ex.Message;
                return View(user);
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            User user = MyUsers.Where(x => x.UserId == id).FirstOrDefault();
            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            User user = new Common.User(collection["UserName"], collection["Password"], collection["Role"]);
            try
            {
                string URL = Startup.APIGatewayHost + "api-user/"+id.ToString();
                var request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "application/json";
                request.Method = "PUT";
                request.Headers["Authorization"] = "Bearer " + httpContextAccessor.HttpContext.User.FindFirst("token").Value;

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(user);
                    streamWriter.Write(json);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewData["status"] = response.StatusDescription;
                        return View(user);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["status"] = ex.Message;
                return View(user);
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            User user = MyUsers.Where(x => x.UserId == id).FirstOrDefault();
            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            User user = MyUsers.Where(x => x.UserId == id).FirstOrDefault();
            try
            {
                string URL = Startup.APIGatewayHost + "api-user/" + id.ToString();
                var request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "application/json";
                request.Method = "Delete";
                request.Headers["Authorization"] = "Bearer " + httpContextAccessor.HttpContext.User.FindFirst("token").Value;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewData["status"] = response.StatusDescription;
                        return View(user);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["status"] = ex.Message;
                return View(user);
            }
        }
    }
}
