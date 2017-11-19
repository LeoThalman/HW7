using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HW7.Models;
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
        string APIKey = System.Web.Configuration.WebConfigurationManager.AppSettings["GiphyAPIKey"];

        // GET: Search
        public JsonResult GetData(string q)
        {
            Debug.WriteLine("sterms: " + q);
            string qurl = "http://api.giphy.com/v1/gifs/search?q=" + q + "&api_key=" + APIKey + "&limit=2";
            WebRequest request = WebRequest.Create(qurl);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Stream dataStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string gifData = reader.ReadToEnd();
            Debug.WriteLine(gifData);
            reader.Close();
            resp.Close();

            JObject gifs = JObject.Parse(gifData);
            IList<JToken> data = gifs["data"].Children().Values("images").Values("fixed_height").ToList();
            IList<ImageData> imagesData = new List<ImageData>();
            foreach(JToken gif in data)
            {
                ImageData image = gif.ToObject<ImageData>();
                imagesData.Add(image);

            }
            Debug.WriteLine("Test Test Test --------");
            foreach (ImageData i in imagesData)
            {
                Debug.WriteLine(1);
                Debug.WriteLine(i.url);

            }
            string gifReturn = JsonConvert.SerializeObject(imagesData, Formatting.Indented);
            return Json(gifReturn, JsonRequestBehavior.AllowGet);
        }

        public void TestRoute(string sterms)
        {
            Debug.WriteLine("Route works");
        }
    }
}