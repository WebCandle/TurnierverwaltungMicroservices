#region Dateikopf
// Autor:       Maher Al Abbasi       
// Datum:      04.02.2021
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
    public class MannschaftController : Controller
    {
        private IHttpContextAccessor httpContextAccessor;
        public List<Mannschaft> Mannschaften { get; set; }
        public MannschaftController(IHttpContextAccessor _httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;
            LoadMannschaften();
        }
        private void LoadMannschaften()
        {
            
            string URL = Startup.APIGatewayHost + "api-mannschaft";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "GET";
            request.Headers["Authorization"] = "Bearer "+ httpContextAccessor.HttpContext.User.FindFirst("token").Value;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                Mannschaften = JsonConvert.DeserializeObject<List<Mannschaft>>(reader.ReadToEnd());
            }
        }
        // GET: MannschaftController
        public ActionResult Index()
        {
            return View(Mannschaften);
        }

        // GET: MannschaftController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MannschaftController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MannschaftController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            Mannschaft mannschaft = new Mannschaft(collection["Name"], Convert.ToDateTime(collection["Gruendungsdatum"]));
            try
            {
                string URL = Startup.APIGatewayHost + "api-mannschaft";
                var request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "application/json";
                request.Method = "POST";
                request.Headers["Authorization"] = "Bearer " + httpContextAccessor.HttpContext.User.FindFirst("token").Value;

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(mannschaft);
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
                        return View(mannschaft);
                    }
                }
            }
            catch(Exception ex)
            {
                ViewData["status"] = ex.Message;
                return View(mannschaft);
            }
        }

        // GET: MannschaftController/Edit/5
        public ActionResult Edit(int id)
        {
            Mannschaft mannschaft = Mannschaften.Where(x => x.MannschaftId == id).FirstOrDefault();
            return View(mannschaft);
        }

        // POST: MannschaftController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            Mannschaft mannschaft = Mannschaften.Where(x => x.MannschaftId == id).FirstOrDefault();
            mannschaft.Name = collection["Name"];
            mannschaft.Gruendungsdatum = Convert.ToDateTime(collection["Gruendungsdatum"]);
            try
            {
                string URL = Startup.APIGatewayHost + "api-mannschaft/"+id.ToString();
                var request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "application/json";
                request.Method = "Put";
                request.Headers["Authorization"] = "Bearer " + httpContextAccessor.HttpContext.User.FindFirst("token").Value;

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(mannschaft);
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
                        return View(mannschaft);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["status"] = ex.Message;
                return View(mannschaft);
            }
        }

        // GET: MannschaftController/Delete/5
        public ActionResult Delete(int id)
        {
            Mannschaft mannschaft = Mannschaften.Where(x => x.MannschaftId == id).FirstOrDefault();
            return View(mannschaft);
        }

        // POST: MannschaftController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            Mannschaft mannschaft = Mannschaften.Where(x => x.MannschaftId == id).FirstOrDefault();
            try
            {
                string URL = Startup.APIGatewayHost + "api-mannschaft/" + id.ToString();
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
                        return View(mannschaft);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["status"] = ex.Message;
                return View(mannschaft);
            }
        }
    }
}
