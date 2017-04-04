using MyMovieWatchlist.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MyMovieWatchlist.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            var getWebApiContent = new GetWebApiContent();
            var ccc = getWebApiContent.GetValue().Result;

            JObject xavier = JObject.Parse(ccc);
            var vvv = xavier.SelectToken("Search").ToString();

            List<Movie> movie = (List<Movie>)JsonConvert.DeserializeObject(vvv, typeof(List<Movie>));
            return View(movie);
        }
    }
}