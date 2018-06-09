using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Web.Mvc;

namespace bonus_api_lab.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult GetFact(string day, string month)
        {
            //1. Build an HTTP Request

            HttpWebRequest request =
                       WebRequest.CreateHttp($"https://numbersapi.p.mashape.com/{day}/{month}/date?fragment=true&json=true");
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            // Adding keys to the header 
            request.Headers.Add("X-Mashape-Key", "bsytHiIt3FmshiXhyAlMdv4D7Vsxp1OeNuxjsnNL5l6EVqH1u9");

            ViewBag.Title = "Get Fact By Date";

            HttpWebResponse Response;
            try
            {
                Response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                ViewBag.Error = "Exception";
                ViewBag.ErrorDescription = e.Message;
                return View();
            }

            if (Response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = Response.StatusCode;
                ViewBag.ErrorDescription = Response.StatusDescription;
                return View();
            }

            StreamReader reader = new StreamReader(Response.GetResponseStream());
            string FactData = reader.ReadToEnd();

            JObject JsonData = JObject.Parse(FactData);
            ViewBag.Year = JsonData["year"];
            ViewBag.Fact = JsonData["text"];



            return View();
        }
        public ActionResult GetRandomFact(string type)
        {
            //1. Build an HTTP Request
            HttpWebRequest request =
                       WebRequest.CreateHttp($"https://numbersapi.p.mashape.com/random/{type}");
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            // Adding keys to the header 
            request.Headers.Add("X-Mashape-Key", "bsytHiIt3FmshiXhyAlMdv4D7Vsxp1OeNuxjsnNL5l6EVqH1u9");

            ViewBag.Title = "Get Fact By Date";

            HttpWebResponse Response;
            try
            {
                Response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                ViewBag.Error = "Exception";
                ViewBag.ErrorDescription = e.Message;
                return View();
            }

            if (Response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = Response.StatusCode;
                ViewBag.ErrorDescription = Response.StatusDescription;
                return View();
            }

            StreamReader reader = new StreamReader(Response.GetResponseStream());
            string FactData = reader.ReadToEnd();

           
            ViewBag.Fact = FactData;
            //ViewBag.Fact = JsonData["text"];



            return View();
        }
    }
}
