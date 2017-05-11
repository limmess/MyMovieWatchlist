using MyMovieWatchlist.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMovieWatchlist.Interfaces
{

    public interface IWebApiRepository
    {
        Task<string> SearchMovieByImdbId(string imdbId);
        Task<string> SearchMovieByName(string movieName);
        IEnumerable<string> AddMoviesInfo(IEnumerable<Movie> moviesImdbIdFromDatabaseList);
        bool ResponseIsValid(string searchResponseInJson);
    }
}