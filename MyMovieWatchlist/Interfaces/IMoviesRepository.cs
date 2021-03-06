using MyMovieWatchlist.Models;
using System.Collections.Generic;

namespace MyMovieWatchlist.Interfaces
{
    public interface IMoviesRepository
    {
        IEnumerable<SiteMenu> SiteMenusList();
        IEnumerable<Movie> MoviesList();
        void AddDirectory(string dirName, int parentDirId);
        void DeleteDirectory(int id);
        void AddMovie(Movie movie);
        void DeleteMovie(int id);
        void SaveChanges();
        Movie FindById(int id);
        Movie FindByImdbId(string imdbId);
    }
}