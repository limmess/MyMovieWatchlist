using MyMovieWatchlist.DAL;
using MyMovieWatchlist.Interfaces;
using MyMovieWatchlist.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyMovieWatchlist.Impl
{
    public class SqlMoviesRepository : IMoviesRepository
    {
        public IEnumerable<Movie> List()
        {
            using (MovieDBContext db = new MovieDBContext())
            {
                return db.Movies.ToList();
            }
        }
    }
}