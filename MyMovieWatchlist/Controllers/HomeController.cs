using MyMovieWatchlist.DAL;
using MyMovieWatchlist.Models;
using MyMovieWatchlist.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MyMovieWatchlist.Controllers
{
    public class HomeController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        public ActionResult Index()
        {
            var fff = new SearchWebApiMovieByImdbIdToList();

            var dbMovieList = db.Movies.ToList();
            var jsonMovieListFromWebApi = fff.GetValue(dbMovieList).Result;
            List<Movie> moviesView = new List<Movie>();

            foreach (var c in jsonMovieListFromWebApi)
            {
                Movie movie = (Movie)JsonConvert.DeserializeObject(c, typeof(Movie));
                moviesView.Add(movie);
            }

            return View(moviesView);
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
        public ActionResult SelectedMovie(string SelectedMovieImdbId)
        {
            var searchWebApiMovieByImdbId = new SearchWebApiMovieByImdbId();
            var selectedMovieJsonString = searchWebApiMovieByImdbId.GetValue(SelectedMovieImdbId).Result;

            //Deserialize Json to movie
            Movie movie = (Movie)JsonConvert.DeserializeObject(selectedMovieJsonString, typeof(Movie));

            //Pass movies list to ViewModel -  MovieSearchedListViewModel
            var movieView = new SelectedMovieDetailsViewModel(movie);

            return View("SelectedMovieDetails", movieView);
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }
    }
}

//[Bind(Include = "Id,ParentId,Title,Year,Rated,Released,Runtime,Genre,Director,Writer,Actors,Plot,Language,Country,Awards,Poster,Metascore,imdbRating,imdbVotes,imdbID,Type,DVD,BoxOffice,Production,Website,Response,SelectedForSave")]