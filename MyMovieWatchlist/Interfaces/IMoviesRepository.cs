using MyMovieWatchlist.Models;
using System.Collections.Generic;

namespace MyMovieWatchlist.Interfaces
{
    public interface IMoviesRepository
    {
        IEnumerable<SiteMenu> SiteMenusList();
        IEnumerable<Movie> MoviesList();
        void Add(Movie movie);
        void Delete(int id);
        void SaveChanges();
        Movie FindById(int id);
    }
}