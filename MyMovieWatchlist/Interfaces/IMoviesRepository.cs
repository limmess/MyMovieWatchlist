using MyMovieWatchlist.Models;
using System.Collections.Generic;

namespace MyMovieWatchlist.Interfaces
{
    public interface IMoviesRepository
    {
        IEnumerable<SiteMenu> SiteMenusList();
        IEnumerable<Movie> MoviesList();
        void AddDirectory(string dirName, int parentDirId);
        void AddMovie(Movie movie);
        void DeleteMovie(int id);
        void SaveChanges();
        Movie FindById(int id);
    }
}