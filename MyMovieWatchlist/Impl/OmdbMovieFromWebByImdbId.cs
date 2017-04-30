using MyMovieWatchlist.Interfaces;
using MyMovieWatchlist.Models;
using Newtonsoft.Json;

namespace MyMovieWatchlist.Impl
{
    public class OmdbMovieFromWebByImdbId : IMovieFromWebByImdbId

    {
        public Movie GetMovie(string imdbId)
        {
            SearchWebApiMovieByImdbId searchWebApiMovieByImdbId = new SearchWebApiMovieByImdbId();
            string searchResult = searchWebApiMovieByImdbId.GetMovie(imdbId).Result;
            Movie movie = (Movie)JsonConvert.DeserializeObject(searchResult, typeof(Movie));
            return movie;
        }
    }
}