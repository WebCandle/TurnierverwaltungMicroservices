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
                Spieler spieler = new Spieler(collection["Name"], collection["Nachname"], Convert.ToDateTime(collection["Geburtsdatum"]), Convert.ToInt32(collection["MannschaftId"]), collection["Aufgabe"]);
                string URL = Startup.APIGatewayHost + "api-person/spieler";
                var request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "application/json";
                request.Method = "POST";
                request.Headers["Authorization"] = "Bearer " + httpContextAccessor.HttpContext.User.FindFirst("token").Value;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(spieler);
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
            catch (Exception ex)
            {
                ViewData["status"] = ex.Message;
                return View();
            }
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

        // GET: PersonController/EditSpieler/5
        public ActionResult EditSpieler(int id)
        {
            Spieler spieler = Personen.Where(x => x.PersonId == id).FirstOrDefault() as Spieler;
            ViewData["mannschaftId"] = spieler.MannschaftId.ToString();
            return View(spieler);
        }

        // POST: PersonController/EditSpieler/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSpieler(int id, IFormCollection collection)
        {
            try
            {
                Spieler spieler = new Spieler(collection["Name"], collection["Nachname"], Convert.ToDateTime(collection["Geburtsdatum"]), Convert.ToInt32(collection["MannschaftId"]), collection["Aufgabe"]);
                spieler.PersonId = id;
                string URL = Startup.APIGatewayHost + "api-person/spieler/"+id.ToString();
                var request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "application/json";
                request.Method = "PUT";
                request.Headers["Authorization"] = "Bearer " + httpContextAccessor.HttpContext.User.FindFirst("token").Value;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(spieler);
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
            catch (Exception ex)
            {
                ViewData["status"] = ex.Message;
                return View();
            }
        }

        // GET: PersonController/EditTrainer/5
        public ActionResult EditTrainer(int id)
        {
            Trainer trainer = Personen.Where(x => x.PersonId == id).FirstOrDefault() as Trainer;
            ViewData["mannschaftId"] = trainer.MannschaftId.ToString();
            return View(trainer);
        }

        // POST: PersonController/EditTrainer/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTrainer(int id, IFormCollection collection)
        {
            try
            {
                Trainer trainer = new Trainer(collection["Name"], collection["Nachname"], Convert.ToDateTime(collection["Geburtsdatum"]), Convert.ToInt32(collection["MannschaftId"]), Convert.ToDecimal(collection["Gehalt"]));
                trainer.PersonId = id;
                string URL = Startup.APIGatewayHost + "api-person/trainer/" + id.ToString();
                var request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "application/json";
                request.Method = "PUT";
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
            catch (Exception ex)
            {
                ViewData["status"] = ex.Message;
                return View();
            }
        }

        // GET: PersonController/Delete/5
        public ActionResult Delete(int id)
        {
            Person person = Personen.Where(x => x.PersonId == id).FirstOrDefault();
            ViewData["mannschaftId"] = person.MannschaftId.ToString();
            return View(person);
        }

        // POST: PersonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            Person person = Personen.Where(x => x.PersonId == id).FirstOrDefault();
            ViewData["mannschaftId"] = person.MannschaftId.ToString();
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
                        return View(person);
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
