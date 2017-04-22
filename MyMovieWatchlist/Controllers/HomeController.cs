using MyMovieWatchlist.DAL;
using MyMovieWatchlist.Models;
using MyMovieWatchlist.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MyMovieWatchlist.Controllers
{
    public class HomeController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        [HttpPost]
        public ActionResult Index(string search)
        {
            var searchWebApiMoviesByName = new SearchWebApiMoviesByName();

            //Gets a search result in JSON (3 objects: Search, totalResults, Response)
            var searchByNameResultsJson = searchWebApiMoviesByName.GetValue(search).Result;

            //Parce to JSON object
            JObject searchByNameResultsJsonObject = JObject.Parse(searchByNameResultsJson);

            //Extract from search response movies - Object "Search"
            var searchByNameResultsString = searchByNameResultsJsonObject.SelectToken("Search").ToString();

            //Deserialize Json to movies list
            List<Movie> movies = (List<Movie>)JsonConvert.DeserializeObject(searchByNameResultsString, typeof(List<Movie>));

            //Pass movies list to ViewModel -  MovieSearchedListViewModel
            var moviesView = new MovieSearchedListViewModel(movies);

            return View("SearchResult", moviesView);
        }

        public ActionResult Index()
        {
            var fff = new SearchWebApiMovieByImdbIdToList();

            var dbMovieList = db.Movies.ToList();
            var jsonMovieListFromWebApi = fff.GetValue(dbMovieList).Result;
            List<Movie> moviesView = new List<Movie>();

            foreach (var c in jsonMovieListFromWebApi)
            {
                Movie movie = (Movie)JsonConvert.DeserializeObject(c, typeof(Movie));
                movie.Id = dbMovieList.Find(m => m.imdbID == movie.imdbID).Id;
                moviesView.Add(movie);
            }

            return View(moviesView);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Route("Home/Movie/{SelectedMovieImdbId}")]
        public ActionResult Movie(string SelectedMovieImdbId)
        {
            var searchWebApiMovieByImdbId = new SearchWebApiMovieByImdbId();
            var selectedMovieJsonString = searchWebApiMovieByImdbId.GetValue(SelectedMovieImdbId).Result;

            //Deserialize Json to movie
            Movie movie = (Movie)JsonConvert.DeserializeObject(selectedMovieJsonString, typeof(Movie));

            //Pass movies list to ViewModel -  MovieSearchedListViewModel
            var movieView = new SelectedMovieDetailsViewModel(movie);

            return View("Movie", movieView);
        }

        public ActionResult Contact()
        {
            var searchWebApiMoviesByName = new SearchWebApiMoviesByName();

            //Gets a search result in JSON (3 objects: Search, totalResults, Response)
            var searchByNameResultsJson = searchWebApiMoviesByName.GetValue("king").Result;

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

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

//[Bind(Include = "Id,ParentId,Title,Year,Rated,Released,Runtime,Genre,Director,Writer,Actors,Plot,Language,Country,Awards,Poster,Metascore,imdbRating,imdbVotes,imdbID,Type,DVD,BoxOffice,Production,Website,Response,SelectedForSave")]