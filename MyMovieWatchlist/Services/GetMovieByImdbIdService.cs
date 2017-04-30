using MyMovieWatchlist.Impl;
using MyMovieWatchlist.Interfaces;
using MyMovieWatchlist.Models;

namespace MyMovieWatchlist.Services
{
    public class GetMovieByImdbIdService
    {
        private readonly IMovieImdbId _movieImdbId;

        public GetMovieByImdbIdService(IMovieImdbId movieImdbId)
        {
            _movieImdbId = movieImdbId;
        }

        public GetMovieByImdbIdService() : this(new OmdbMovieRepository())
        {
        }

        public SearchImdbIdResult Extract(string imdbId)
        {
            SearchImdbIdResult result = new SearchImdbIdResult { MovieResult = _movieImdbId.GetMovie(imdbId) };
            return result;
        }
    }
}