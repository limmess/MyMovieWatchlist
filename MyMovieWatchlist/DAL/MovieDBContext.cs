using MyMovieWatchlist.Models;
using System.Data.Entity;

namespace MyMovieWatchlist.DAL
{
    public class MovieDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<SiteMenu> SiteMenus { get; set; }
    }
}
