using GiphyWorld.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GiphyWorld.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var apiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["giphyAPIKey"];
            var query = "pikachu";
            var limit = "5";
            var offset = "0";
            var rating = "G";
            var lang = "en";

            // Create the request to the API
            WebRequest request = WebRequest.Create($"https://api.giphy.com/v1/gifs/search?api_key={apiKey}&q={query}&limit={limit}&offset={offset}&rating={rating}&lang={lang}");
            // Send the request.
            WebResponse response = request.GetResponse();
            // Get back the response stream.
            Stream stream = response.GetResponseStream();
            // Make the stream reacheable/accessible.
            StreamReader reader = new StreamReader(stream);
            // Human readable response.
            string responseString = reader.ReadToEnd();
            // Parses the response string.
            JObject parsedString = JObject.Parse(responseString);
            // Map the parsed string to our Pokemon model.
            GiphySearchResultModel searchResult = parsedString.ToObject<GiphySearchResultModel>();

            return View(searchResult);
        }

    }
}