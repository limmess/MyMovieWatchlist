using MyMovieWatchlist.DAL;
using MyMovieWatchlist.Impl;
using MyMovieWatchlist.Models;
using MyMovieWatchlist.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace MyMovieWatchlist.Controllers
{
    public class HomeController : Controller
    {
        private MovieDBContext db = new MovieDBContext();
        private SearchWebApiMoviesByName searchWebApiMoviesByName = new SearchWebApiMoviesByName();
        private ParseSearchResultToMoviesList _parseSearchResultToMoviesList = new ParseSearchResultToMoviesList();
        private SearchWebApiMovieByImdbIdToList searchWebApiMovieByImdbIdToList = new SearchWebApiMovieByImdbIdToList();

        [HttpPost]
        public ActionResult Index(string search)
        {
            //Search  movie. Result JSON (3 objects: Search, totalResults, Response)
            var searchByNameResultsJson = searchWebApiMoviesByName.GetValue(search).Result;

            //Converts JSON search result to movie list
            var movies = _parseSearchResultToMoviesList.Parse(searchByNameResultsJson);

            //Pass movies list to ViewModel -  MovieSearchedListViewModel
            var moviesView = new MovieSearchedListViewModel(movies);

            return View("SearchResult", moviesView);
        }

        public ActionResult Index()
        {
            //var dbMovieList = db.Movies.ToList();
            var sqlMovieRepository = new SqlMovieRepository();
            var dbMovieList1 = (List<Movie>)sqlMovieRepository.List();

            var jsonMovieListFromWebApi = searchWebApiMovieByImdbIdToList.GetValue(dbMovieList1).Result;

            var convertJsonMovieList = new ConvertJsonMovieList();
            var moviesView = convertJsonMovieList.Convert(jsonMovieListFromWebApi);

            //List<Movie> moviesView = new List<Movie>();

            //foreach (var c in jsonMovieListFromWebApi)
            //{
            //    Movie movie = (Movie)JsonConvert.DeserializeObject(c, typeof(Movie));
            //    movie.Id = dbMovieList.Find(m => m.imdbID == movie.imdbID).Id;
            //    moviesView.Add(movie);
            //}

            return View(moviesView);
        }

        [Route("Home/Movie/{SelectedMovieImdbId}")]
        public ActionResult Movie(string SelectedMovieImdbId)
        {
            var searchWebApiMovieByImdbId = new SearchWebApiMovieByImdbId();
            var selectedMovieJsonString = searchWebApiMovieByImdbId.GetMovie(SelectedMovieImdbId).Result;

            //Deserialize Json to movie
            Movie movie = (Movie)JsonConvert.DeserializeObject(selectedMovieJsonString, typeof(Movie));

            //Pass movies list to ViewModel -  MovieSearchedListViewModel
            var movieView = new SelectedMovieDetailsViewModel(movie);

            return View("Movie", movieView);
        }

        [HttpPost]
        public ActionResult SelectedMovie(string SelectedMovieImdbId)
        {
            var searchWebApiMovieByImdbId = new SearchWebApiMovieByImdbId();
            var selectedMovieJsonString = searchWebApiMovieByImdbId.GetMovie(SelectedMovieImdbId).Result;

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