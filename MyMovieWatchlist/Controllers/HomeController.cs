using MyMovieWatchlist.DAL;
using MyMovieWatchlist.Models;
using MyMovieWatchlist.Services;
using MyMovieWatchlist.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace MyMovieWatchlist.Controllers
{
    public class HomeController : Controller
    {
        private MovieDBContext db = new MovieDBContext();
        private ExtractAllService myServiceAll = new ExtractAllService();
        private ExtractOneService myServiceOne = new ExtractOneService();
        private SearchWebApiMoviesByName searchWebApiMoviesByName = new SearchWebApiMoviesByName();
        private ParseSearchResultToMoviesList parseSearchResultToMoviesList = new ParseSearchResultToMoviesList();

        [HttpPost]
        public ActionResult Index(string search)
        {
            //Search  movie. Result JSON (3 objects: Search, totalResults, Response)
            string searchByNameResultsJson = searchWebApiMoviesByName.GetValue(search).Result;

            //Converts JSON search result to movie list
            List<Movie> movies = parseSearchResultToMoviesList.Parse(searchByNameResultsJson);

            //Pass movies list to ViewModel -  MovieSearchedListViewModel
            MovieSearchedListViewModel moviesView = new MovieSearchedListViewModel(movies);

            return View("SearchResult", moviesView);
        }

        public ActionResult Index()
        {
            List<Movie> moviesView = myServiceAll.ExtractAll();
            return View(moviesView);
        }

        [Route("Home/Movie/{SelectedMovieImdbId}")]
        public ActionResult Movie(string SelectedMovieImdbId)
        {
            Movie movie = myServiceOne.ExtractOne(SelectedMovieImdbId);
            SelectedMovieDetailsViewModel movieView = new SelectedMovieDetailsViewModel(movie);
            return View("Movie", movieView);
        }

        [HttpPost]
        public ActionResult SelectedMovie(string SelectedMovieImdbId)
        {
            Movie movie = myServiceOne.ExtractOne(SelectedMovieImdbId);
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