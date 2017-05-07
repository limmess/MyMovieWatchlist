using MyMovieWatchlist.DAL;
using MyMovieWatchlist.Interfaces;
using MyMovieWatchlist.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyMovieWatchlist.Impl
{
    public class SqlMoviesRepository : IMoviesRepository
    {
        private readonly MovieDBContext _db = new MovieDBContext();

        public IEnumerable<Movie> List()
        {
            return _db.Movies.ToList();
        }

        public Movie FindById(int id)
        {
            var movie = _db.Movies.Find(id);
            return movie;
        }

        public void Add(Movie movie)
        {
            _db.Movies.Add(movie);
        }

        public void Delete(int id)
        {
            Movie movie = FindById(id);
            _db.Movies.Remove(movie);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}