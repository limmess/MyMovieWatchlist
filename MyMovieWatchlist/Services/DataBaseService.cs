using MyMovieWatchlist.Impl;
using MyMovieWatchlist.Interfaces;
using MyMovieWatchlist.Models;
using System.Collections.Generic;

namespace MyMovieWatchlist.Services
{
    public class DatabaseService
    {
        private readonly IMoviesRepository _moviesRepository;

        public DatabaseService(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        /// <summary>
        /// Constructor. Initializes SqlMoviesRepository
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
            Movie movie = _moviesRepository.FindById(id);
            return movie;
        }

        /// <summary>
        /// Read all movies from database
        /// </summary>
        /// <returns>Movie list</returns>
        public IEnumerable<Movie> ReadAllMoviesFromDatabase()
        {
            IEnumerable<Movie> result = _moviesRepository.MoviesList();
            return result;
        }

        /// <summary>
        /// Read all directories from database
        /// </summary>
        /// <returns>Direcotry list</returns>
        public IEnumerable<SiteMenu> ReadSiteMenusFromDatabse()
        {
            IEnumerable<SiteMenu> result = _moviesRepository.SiteMenusList();
            return result;
        }





    }
}