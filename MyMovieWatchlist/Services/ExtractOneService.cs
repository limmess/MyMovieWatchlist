using MyMovieWatchlist.Impl;
using MyMovieWatchlist.Interfaces;
using MyMovieWatchlist.Models;

namespace MyMovieWatchlist.Services
{
    public class ExtractOneService
    {
        private readonly IMovieFromWebByImdbId _movieFromWebByImdbId;

        private ExtractOneService(IMovieFromWebByImdbId movieFromWebByImdbId)
        {
            _movieFromWebByImdbId = movieFromWebByImdbId;
        }

        public ExtractOneService() : this(new OmdbMovieFromWebByImdbId())
        {
        }

        public Movie ExtractOne(string imdbId)
        {
            Movie movie = _movieFromWebByImdbId.GetMovie(imdbId);
            return movie;
        }
    }
}