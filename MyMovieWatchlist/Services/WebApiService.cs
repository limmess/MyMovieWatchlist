using MyMovieWatchlist.Impl;
using MyMovieWatchlist.Interfaces;
using MyMovieWatchlist.Models;
using System.Collections.Generic;

namespace MyMovieWatchlist.Services
{
    public class WebApiService
    {
        private readonly IWebApiRepository _webApiRepository;

        public WebApiService(IWebApiRepository webApiRepository)
        {
            _webApiRepository = webApiRepository;
        }

        public WebApiService() : this(new OmdbRepository())
        {
        }

        public string SearchMovieByImdbId(string imdbId)
        {
            string result = _webApiRepository.SearchMovieByImdbId(imdbId).Result;
            return result;
        }

        public string SearchMovieByName(string movieName)
        {
            string result = _webApiRepository.SearchMovieByName(movieName).Result;
            return result;
        }

        public IEnumerable<string> AddMoviesInfo(IEnumerable<Movie> moviesFromDatabaseList)
        {
            IEnumerable<string> moviesWithFullInfoJsonList = _webApiRepository.AddMoviesInfo(moviesFromDatabaseList);
            return moviesWithFullInfoJsonList;
        }



    }
}