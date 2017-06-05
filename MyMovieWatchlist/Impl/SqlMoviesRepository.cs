using MyMovieWatchlist.DAL;
using MyMovieWatchlist.Interfaces;
using MyMovieWatchlist.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace MyMovieWatchlist.Impl
{
    public class SqlMoviesRepository : IMoviesRepository
    {
        private readonly MovieDBContext _db = new MovieDBContext();

        public IEnumerable<Movie> MoviesList()
        {
            return _db.Movies.ToList();
        }

        public Movie FindById(int id)
        {
            Movie movie = _db.Movies.Find(id);
            return movie;
        }

        public Movie FindByImdbId(string imdbId)
        {
            Movie movie = _db.Movies.FirstOrDefault(m => m.imdbID == imdbId);

            return movie;
        }

        public void AddMovie(Movie movie)
        {
            _db.Movies.Add(movie);
        }

        public void DeleteMovie(int id)
        {
            Movie movie = FindById(id);
            _db.Movies.Remove(movie);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public IEnumerable<SiteMenu> SiteMenusList()
        {
            var result = _db.SiteMenus.Include(m => m.Movies).ToList();
            return result;
        }

        public void AddDirectory(string dirName, int parentDirId)
        {
            string navUrl = "/";
            if (parentDirId == -1)
            {
                _db.SiteMenus.Add(new SiteMenu() { MenuName = dirName, ParentMenuId = 0, NavUrl = navUrl + dirName });
            }
            else
            {
                SiteMenu parentDirecotry = _db.SiteMenus.Find(parentDirId);

                StringBuilder newNavUrl = new StringBuilder(parentDirecotry.NavUrl);
                navUrl = newNavUrl.Append("/" + dirName).ToString();

                _db.SiteMenus.Add(new SiteMenu() { MenuName = dirName, ParentMenuId = parentDirId, NavUrl = navUrl });
            }
        }

        public void DeleteDirectory(int id)
        {
            SiteMenu siteMenuToDelete = _db.SiteMenus.First(m => m.Id == id);
            _db.SiteMenus.Remove(siteMenuToDelete);
        }
    }
}