using MyMovieWatchlist.Impl;
using MyMovieWatchlist.Interfaces;
using MyMovieWatchlist.Models;
using System.Collections.Generic;

namespace MyMovieWatchlist.Services
{
    public class ExtractAllService
    {
        private readonly IMoviesRepository _moviesRepository;

        private ExtractAllService(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        public ExtractAllService() : this(new SqlMoviesRepository())
        {
        }

        public ExtractionResult Extract()
        {
            ExtractionResult result = new ExtractionResult { Movies = _moviesRepository.List() };
            return result;
        }

        public List<Movie> ExtractAll()
        {
            SearchWebApiMovieByImdbIdToList searchWebApiMovieByImdbIdToList = new SearchWebApiMovieByImdbIdToList();
            ConvertJsonMovieList convertJsonMovieList = new ConvertJsonMovieList();

            IEnumerable<Movie> dbMovieList = _moviesRepository.List();
            List<string> jsonMovieListFromWebApi = searchWebApiMovieByImdbIdToList.GetValue((List<Movie>)dbMovieList).Result;
            List<Movie> moviesView = convertJsonMovieList.Convert(jsonMovieListFromWebApi);
            return moviesView;
        }
    }
}