﻿using MyMovieWatchlist.Impl;
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


        public ActionResult Simple()
        {

            //List<SiteMenu> all = new List<SiteMenu>();
            //using (MovieDBContext dc = new MovieDBContext())
            //{
            //    all = dc.SiteMenus.OrderBy(a => a.ParentMenuId).Include(m => m.Movies).ToList();
            //}
            IEnumerable<SiteMenu> directories = _myDatabaseService.ReadSiteMenusFromDatabse();
            List<SiteMenu> directoriesSorted = directories.OrderBy(a => a.ParentMenuId).ToList();


            foreach (var directory in directoriesSorted)
            {
                List<Movie> movieList = directory.Movies.ToList();

                foreach (var movie in movieList)
                {
                    var searchResult = _myWebApiService.SearchMovieByImdbId(movie.imdbID);

                    //Deserialize found movie in JSON format to Movie object
                    Movie movie1 = (Movie)JsonConvert.DeserializeObject(searchResult, typeof(Movie));

                    movie.Title = movie1.Title;
                }
            }


            return View(directoriesSorted);
        }


        public ActionResult Index()
        {
            IEnumerable<SiteMenu> directories = _myDatabaseService.ReadSiteMenusFromDatabse();
            List<SiteMenu> directoriesSorted = directories.OrderBy(a => a.ParentMenuId).ToList();
            IEnumerable<Movie> moviesFromDatabaseOnlyImdbId = _myDatabaseService.ReadAllMoviesFromDatabase();
            IEnumerable<string> moviesFromDatabaseWithInfoFromWebJson = _myWebApiService.AddMoviesInfo(moviesFromDatabaseOnlyImdbId);
            IEnumerable<Movie> movies = _convertJsonToMovieList.Convert((List<string>)moviesFromDatabaseWithInfoFromWebJson);


            foreach (var directory in directoriesSorted)
            {
                List<Movie> movieList = directory.Movies.ToList();

                foreach (Movie movie in movieList)
                {
                    movie.Title = movies.Single(a => a.imdbID == movie.imdbID).Title;
                }
            }

            return View(directoriesSorted);
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

            var movieFromDb = _myDatabaseService.ReadOneMovieFromDatabase(SelectedMovieImdbId);
            movie.Id = movieFromDb.Id;

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
        public ActionResult DeleteMovie(int? id)
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
            movieFull.Id = id.Value;
            return View(movieFull);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("DeleteMovie")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMovieConfirmed(int id)
        {
            _myDatabaseService.DeleteMovie(id);
            _myDatabaseService.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult CreateDir(string dirName, string parentMenuId)
        {
            _myDatabaseService.AddDirectory(dirName, int.Parse(parentMenuId));
            _myDatabaseService.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult DeleteDir(string directoryToDeleteId)
        {
            _myDatabaseService.DeleteDirectory(int.Parse(directoryToDeleteId));
            _myDatabaseService.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}