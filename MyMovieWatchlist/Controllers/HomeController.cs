using MyMovieWatchlist.Models;
using MyMovieWatchlist.Services;
using MyMovieWatchlist.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace MyMovieWatchlist.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebApiService _myWebApiService = new WebApiService();
        private readonly DatabaseService _myDatabaseService = new DatabaseService();
        private readonly ParseSearchResultToMoviesList _parseSearchResultToMoviesList = new ParseSearchResultToMoviesList();

        public ActionResult Index()
        {
            IEnumerable<Movie> tt = _myDatabaseService.ReadAllMoviesFromDatabase1();
            IEnumerable<string> yy = _myWebApiService.AddMoviesInfo(tt);
            List<Movie> moviesView = _myDatabaseService.ReadAllMoviesFromDatabaseAddWebApiInfo();
            return View(moviesView);
        }

        [HttpPost]
        public ActionResult Index(string search)
        {
            //Search  movie. Result JSON (3 objects: Search, totalResults, Response)
            string searchByNameResultsJson = _myWebApiService.SearchMovieByName(search);


            //Converts JSON search result to movie list
            List<Movie> movies = _parseSearchResultToMoviesList.Parse(searchByNameResultsJson);

            //Pass movies list to ViewModel -  MovieSearchedListViewModel
            MovieSearchedListViewModel moviesView = new MovieSearchedListViewModel(movies);

            return View("SearchResult", moviesView);
        }

        [Route("Home/Movie/{SelectedMovieImdbId}")]
        public ActionResult Movie(string SelectedMovieImdbId)
        {
            string searchResult = _myWebApiService.SearchMovieByImdbId(SelectedMovieImdbId);
            Movie movie = (Movie)JsonConvert.DeserializeObject(searchResult, typeof(Movie));
            SelectedMovieDetailsViewModel movieView = new SelectedMovieDetailsViewModel(movie);
            return View("Movie", movieView);
        }

        [HttpPost]
        public ActionResult SelectedMovie(string SelectedMovieImdbId)
        {

            string searchResult = _myWebApiService.SearchMovieByImdbId(SelectedMovieImdbId);
            Movie movie = (Movie)JsonConvert.DeserializeObject(searchResult, typeof(Movie));
            SelectedMovieDetailsViewModel movieView = new SelectedMovieDetailsViewModel(movie);
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
                _myDatabaseService.AddMovie(movie);
                _myDatabaseService.SaveChanges();
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

            Movie movie = _myDatabaseService.ReadOneMovieFromDatabase(id.Value);

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
            _myDatabaseService.DeleteMovie(id);
            _myDatabaseService.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

//[Bind(Include = "Id,ParentId,Title,Year,Rated,Released,Runtime,Genre,Director,Writer,Actors,Plot,Language,Country,Awards,Poster,Metascore,imdbRating,imdbVotes,imdbID,Type,DVD,BoxOffice,Production,Website,Response,SelectedForSave")]