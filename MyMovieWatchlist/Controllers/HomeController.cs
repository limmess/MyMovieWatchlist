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
        private OmdbMovieRepository omdbMovieRepository = new OmdbMovieRepository();

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
            SqlMovieRepository sqlMovieRepository = new SqlMovieRepository();
            List<Movie> dbMovieList = (List<Movie>)sqlMovieRepository.List();

            List<string> jsonMovieListFromWebApi = searchWebApiMovieByImdbIdToList.GetValue(dbMovieList).Result;

            ConvertJsonMovieList convertJsonMovieList = new ConvertJsonMovieList();
            List<Movie> moviesView = convertJsonMovieList.Convert(jsonMovieListFromWebApi);

            return View(moviesView);
        }

        [Route("Home/Movie/{SelectedMovieImdbId}")]
        public ActionResult Movie(string SelectedMovieImdbId)
        {
            Movie movie = omdbMovieRepository.GetMovie(SelectedMovieImdbId);
            SelectedMovieDetailsViewModel movieView = new SelectedMovieDetailsViewModel(movie);
            return View("Movie", movieView);
        }

        [HttpPost]
        public ActionResult SelectedMovie(string SelectedMovieImdbId)
        {
            SearchWebApiMovieByImdbId searchWebApiMovieByImdbId = new SearchWebApiMovieByImdbId();
            string selectedMovieJsonString = searchWebApiMovieByImdbId.GetMovie(SelectedMovieImdbId).Result;

            //Deserialize Json to movie
            Movie movie = (Movie)JsonConvert.DeserializeObject(selectedMovieJsonString, typeof(Movie));

            //Pass movies list to ViewModel -  MovieSearchedListViewModel
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