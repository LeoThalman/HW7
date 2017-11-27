using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HW7.Models;
using HW7.DAL;
using System.Web.Mvc;
using System.Net;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace HW7.Controllers
{    

    public class SearchController : Controller
    {
        //Database to log requests
        private RequestDBContext db = new RequestDBContext();

        //API key for giphy
        private string APIKey = System.Web.Configuration.WebConfigurationManager.AppSettings["GiphyAPIKey"];

        /// <summary>
        /// Takes a string of search terms, as well as a limit of images and a type of image
        /// and then queries the Giphy API to get the image data and then pulls the data out of
        /// the giphy data and puts it in a new json object and passes it to the view
        /// </summary>
        /// <param name="q">terms to search giphy for</param>
        /// <param name="lim">how many gifs to pull up</param>
        /// <param name="gifType">what kind of image to return</param>
        /// <returns>The requested gifs in a json object</returns>
        public JsonResult GetData(string q, string lim, string gifType)
        {
            LogRequest(q);
            string qurl = "http://api.giphy.com/v1/gifs/search?q=" + q + "&api_key=" + APIKey + "&limit=" + lim;
            WebRequest request = WebRequest.Create(qurl);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Stream dataStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string gifData = reader.ReadToEnd();
            reader.Close();
            resp.Close();
            dataStream.Close();

            JObject gifs = JObject.Parse(gifData);
            IList<JToken> data = gifs["data"].Children().Values("images").Values(gifType).ToList();
            IList<ImageData> imagesData = new List<ImageData>();
            foreach(JToken gif in data)
            {
                ImageData image = gif.ToObject<ImageData>();
                imagesData.Add(image);

            }
            string gifReturn = JsonConvert.SerializeObject(imagesData, Formatting.Indented);
            return Json(gifReturn, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Logs an entry in the request database detailing what the user searched for
        /// as well as their browser and ip address
        /// </summary>
        /// <param name="q">terms the user searched for</param>
        private void LogRequest(string q)
        {
            Request tempLog = new Request
            {
                SearchTerms = q,
                RequestedOn = DateTime.Now,
                UserBrowser = Request.UserAgent,
                UserAddress = Request.UserHostAddress
            };
            db.Requests.Add(tempLog);
            db.SaveChanges();
        }
    }
}