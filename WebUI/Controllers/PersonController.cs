using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebUI.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private IHttpContextAccessor httpContextAccessor;
        public IList<Person> Personen { get; set; }

        public PersonController(IHttpContextAccessor _httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;
            loadPersonen();
        }
        private void loadPersonen()
        {
            string URL = Startup.APIGatewayHost + "api-person";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "GET";
            request.Headers["Authorization"] = "Bearer " + httpContextAccessor.HttpContext.User.FindFirst("token").Value;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                Personen = JsonConvert.DeserializeObject<List<Person>>(reader.ReadToEnd(), new PersonConverter());
            }
        }
        // GET: PersonController
        public ActionResult Index(int mannschaftId = -1)
        {
            ViewData["mannschaftId"] = mannschaftId;
            if (mannschaftId == -1 )
            {
                return RedirectToAction("Index","Mannschaft");
            }
            else
            {
                return View(Personen.Where(x => x.MannschaftId == mannschaftId).ToList());
            }
        }

        // GET: PersonController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PersonController/CreateSpieler
        public ActionResult CreateSpieler(int mannschaftId)
        {
            ViewData["mannschaftId"] = mannschaftId;
            return View();
        }

        // GET: PersonController/CreateSpieler
        public ActionResult CreateTrainer(int mannschaftId)
        {
            ViewData["mannschaftId"] = mannschaftId;
            return View();
        }

        // POST: PersonController/CreateSpieler
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSpieler(IFormCollection collection)
        {
            try
            {
            }
            catch
            {
            }
            return RedirectToAction(nameof(Index), collection["mannschaftId"]);
        }

        // POST: PersonController/CreateTrainer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTrainer(IFormCollection collection)
        {
            try
            {
                Trainer trainer = new Trainer(collection["Name"], collection["Nachname"], Convert.ToDateTime(collection["Geburtsdatum"]), Convert.ToInt32(collection["MannschaftId"]), Convert.ToDecimal(collection["Gehalt"]));
                string URL = Startup.APIGatewayHost + "api-person/trainer";
                var request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "application/json";
                request.Method = "POST";
                request.Headers["Authorization"] = "Bearer " + httpContextAccessor.HttpContext.User.FindFirst("token").Value;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(trainer);
                    streamWriter.Write(json);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return RedirectToAction(nameof(Index), new { mannschaftId = collection["mannschaftId"] });
                    }
                    else
                    {
                        ViewData["status"] = response.StatusDescription;
                        return View();
                    }
                }
            }
            catch(Exception ex)
            {
                ViewData["status"] =ex.Message;
                return View();
            }
        }

        // GET: PersonController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Delete/5
        public ActionResult Delete(int id)
        {
            Person person = Personen.Where(x => x.PersonId == id).FirstOrDefault();
            return View(person);
        }

        // POST: PersonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            Person person = Personen.Where(x => x.PersonId == id).FirstOrDefault();
            try
            {
                string URL = Startup.APIGatewayHost + "api-person/delete/" + id.ToString();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "DELETE";
                request.Headers["Authorization"] = "Bearer " + httpContextAccessor.HttpContext.User.FindFirst("token").Value;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    string state = reader.ReadToEnd();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return RedirectToAction(nameof(Index), new { mannschaftId = person.MannschaftId });
                    }
                    else
                    {
                        ViewData["status"] = response.StatusDescription;
                        return View();
                    }
                }
            }
            catch(Exception ex)
            {
                ViewData["status"] = ex.Message;
                return View(person);
            }
        }
    }
}
