using MyMovieWatchlist.Impl;
using MyMovieWatchlist.Interfaces;
using MyMovieWatchlist.Models;
using System.Collections.Generic;

namespace MyMovieWatchlist.Services
{
    public class DatabaseService
    {
        private readonly IMoviesRepository _moviesRepository;

        private DatabaseService(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DatabaseService() : this(new SqlMoviesRepository())
        {
        }

        /// <summary>
        /// Add movie to database
        /// </summary>
        /// <param name="movie">Movie to save to database</param>
        public void AddMovie(Movie movie)
        {
            _moviesRepository.Add(movie);
        }

        /// <summary>
        /// Delete movie from database
        /// </summary>
        /// <param name="id">Movie ID in database</param>
        public void DeleteMovie(int id)
        {
            _moviesRepository.Delete(id);
        }

        /// <summary>
        /// Save changes to database
        /// </summary>
        public void SaveChanges()
        {
            _moviesRepository.SaveChanges();
        }

        /// <summary>
        /// Read one movie from database
        /// </summary>
        /// <param name="id">Movies id in database</param>
        /// <returns>Movie</returns>
        public Movie ReadOneMovieFromDatabase(int id)
        {
            var movie = _moviesRepository.FindById(id);
            return movie;
        }

        public ExtractionResult ReadAllMoviesFromDatabase()
        {
            ExtractionResult result = new ExtractionResult { Movies = _moviesRepository.List() };
            return result;
        }

        /// <summary>
        /// Reads all movies from database and reads additional movie info from Web API
        /// </summary>
        /// <returns> Movies list</returns>
        public List<Movie> ReadAllMoviesFromDatabaseAddWebApiInfo()
        {
            SearchWebApiMovieByImdbIdToList searchWebApiMovieByImdbIdToList = new SearchWebApiMovieByImdbIdToList();
            ConvertJsonMovieList convertJsonMovieList = new ConvertJsonMovieList();

            IEnumerable<Movie> dbMovieList = _moviesRepository.List();
            List<string> jsonMovieListFromWebApi = searchWebApiMovieByImdbIdToList.GetValue((List<Movie>)dbMovieList).Result;
            List<Movie> moviesView = convertJsonMovieList.Convert(jsonMovieListFromWebApi);
            return moviesView;
        }
    }
}