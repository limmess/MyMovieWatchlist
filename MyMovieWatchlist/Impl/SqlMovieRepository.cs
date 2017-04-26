using MyMovieWatchlist.DAL;
using MyMovieWatchlist.Interfaces;
using MyMovieWatchlist.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyMovieWatchlist.Impl
{
    public class SqlMovieRepository : IMoviesRepository
    {
        public IEnumerable<Movie> List()
        {
            using (var db = new MovieDBContext())
            {
                return db.Movies.ToList();
            }
        }
    }
}