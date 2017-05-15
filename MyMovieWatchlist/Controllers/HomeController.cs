using MyMovieWatchlist.Impl;
using MyMovieWatchlist.Models;
using MyMovieWatchlist.Services;
using MyMovieWatchlist.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MyMovieWatchlist.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebApiService _myWebApiService = new WebApiService();
        private readonly DatabaseService _myDatabaseService = new DatabaseService();
        private readonly ParseSearchResultToMoviesList _parseSearchResultToMoviesList = new ParseSearchResultToMoviesList();
        private readonly ConvertJsonToMovieList _convertJsonToMovieList = new ConvertJsonToMovieList();
        private readonly ErrorViewModel _error = new ErrorViewModel();

        public ActionResult Index()
        {
            IEnumerable<Movie> moviesFromDatabaseOnlyImdbId = _myDatabaseService.ReadAllMoviesFromDatabase();
            IEnumerable<string> moviesFromDatabaseWithInfoFromWebJson = _myWebApiService.AddMoviesInfo(moviesFromDatabaseOnlyImdbId);
            IEnumerable<Movie> movies = _convertJsonToMovieList.Convert((List<string>)moviesFromDatabaseWithInfoFromWebJson);
            return View(movies);
        }

        [HttpPost]
        public ActionResult Index(string search)
        {
            //Search  movie. Result in JSON. Success=(3 objects: Search, totalResults, Response). Error=(2 objects: Response, Error)
            string searchByNameResultInJson = _myWebApiService.SearchMovieByName(search);

            if (!_myWebApiService.ResponseIsValid(searchByNameResultInJson))
            {
                _error.ErrorMessage = "Movie Not Found !!!";
                return View("Error", _error);
            }

            //Convert JSON search result to movie list
            List<Movie> movies = _parseSearchResultToMoviesList.Parse(searchByNameResultInJson);

            //Pass movies list to ViewModel -  MovieSearchedListViewModel
            MovieSearchedListViewModel moviesView = new MovieSearchedListViewModel(movies);

            return View("SearchResult", moviesView);
        }

        [Route("Home/Movie/{SelectedMovieImdbId}")]
        public ActionResult Movie(string SelectedMovieImdbId)
        {

            //prevent to view movies that are not in MyWatchlist
            try
            {
                var tt = _myDatabaseService.ReadAllMoviesFromDatabase().SingleOrDefault(m => m.imdbID == SelectedMovieImdbId).imdbID;
            }
            catch (Exception e)
            {
                _error.ErrorMessage = "Movie is not in MyWatchlist";
                return View("Error", _error);
            }

            //Search  movie by ImdbId
            string searchResult = _myWebApiService.SearchMovieByImdbId(SelectedMovieImdbId);

            if (!_myWebApiService.ResponseIsValid(searchResult))
            {
                _error.ErrorMessage = "Incorrect IMDb ID";
                return View("Error", _error);
            }

            //Deserialize found movie in JSON format to Movie object
            Movie movie = (Movie)JsonConvert.DeserializeObject(searchResult, typeof(Movie));

            //Pass movie to SelectedMovieDetailsViewModel
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
        public ActionResult Save([Bind(Include = "Id,ParentId,Title,Year,Rated,Released,Runtime,Genre,Director,Writer,Actors,Plot,Language,Country,Awards,Poster,Metascore,imdbRating,imdbVotes,imdbID,Type,DVD,BoxOffice,Production,Website,Response,SelectedForSave")]Movie movie)
        {
            if (_myDatabaseService.ReadAllMoviesFromDatabase().Any(m => m.imdbID == movie.imdbID))
            {
                _error.ErrorMessage = "Movie is already in MyWatchlist";
                return View("Error", _error);
            }
            _myDatabaseService.AddMovie(movie);
            _myDatabaseService.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Movie movieInDb = _myDatabaseService.ReadOneMovieFromDatabase(id.Value);

            if (movieInDb == null)
            {
                return HttpNotFound();
            }

            //Search  movie by ImdbId
            string searchResult = _myWebApiService.SearchMovieByImdbId(movieInDb.imdbID);

            //Deserialize found movie in JSON format to Movie object
            Movie movieFull = (Movie)JsonConvert.DeserializeObject(searchResult, typeof(Movie));

            return View(movieFull);
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