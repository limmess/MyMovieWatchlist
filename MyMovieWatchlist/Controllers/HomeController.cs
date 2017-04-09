using MyMovieWatchlist.Models;
using MyMovieWatchlist.ViewModels;
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
            var searchWebApiMoviesByName = new SearchWebApiMoviesByName();

            //Gets a search result in JSON (3 objects: Search, totalResults, Response)
            var searchByNameResultsJson = searchWebApiMoviesByName.GetValue().Result;

            //Parce to JSON object
            JObject searchByNameResultsJsonObject = JObject.Parse(searchByNameResultsJson);

            //Extract from search response movies - Object "Search"
            var searchByNameResultsString = searchByNameResultsJsonObject.SelectToken("Search").ToString();

            //Deserialize Json to movies list
            List<Movie> movies = (List<Movie>)JsonConvert.DeserializeObject(searchByNameResultsString, typeof(List<Movie>));

            //Pass movies list to ViewModel -  MovieSearchedListViewModel
            var moviesView = new MovieSearchedListViewModel(movies);

            return View(moviesView);
        }

        [HttpPost]
        public ActionResult Create(string SelectedMovieImdbId)
        {
            var searchWebApiMovieByImdbId = new SearchWebApiMovieByImdbId();
            var selectedMovieJsonString = searchWebApiMovieByImdbId.GetValue(SelectedMovieImdbId).Result;

            //Deserialize Json to movie
            Movie movie = (Movie)JsonConvert.DeserializeObject(selectedMovieJsonString, typeof(Movie));

            //Pass movies list to ViewModel -  MovieSearchedListViewModel
            var movieView = new SelectedMovieDetailsViewModel(movie);

            return View("SelectedMovieDetails", movieView);
        }

    }
}