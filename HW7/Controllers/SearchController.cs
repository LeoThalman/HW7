using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;

namespace HW7.Controllers
{
    public class SearchController : Controller
    {
        string APIKey = System.Web.Configuration.WebConfigurationManager.AppSettings["GiphyAPIKey"];

        // GET: Search
        public async Task<JsonResult> GetData(string q)
        {
            Debug.WriteLine("sterms: " + q);
            string qurl = "http://api.giphy.com/v1/gifs/search?q=" + q + "&api_key=" + APIKey + "&limit=1";
            WebRequest request = WebRequest.Create(qurl);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Stream data = resp.GetResponseStream();
            StreamReader reader = new StreamReader(data);
            string gifdata = reader.ReadToEnd();

            Debug.WriteLine(gifdata);
            
            return Json(gifdata, JsonRequestBehavior.AllowGet);
        }

        public void TestRoute(string sterms)
        {
            Debug.WriteLine("Route works");
        }
    }
}