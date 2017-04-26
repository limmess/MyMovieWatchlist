using MyMovieWatchlist.Models;
using System.Collections.Generic;

namespace MyMovieWatchlist.Interfaces
{
    public interface IMoviesRepository
    {
        IEnumerable<Movie> List();
    }
}