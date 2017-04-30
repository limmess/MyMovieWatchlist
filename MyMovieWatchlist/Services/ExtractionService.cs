using MyMovieWatchlist.Impl;
using MyMovieWatchlist.Interfaces;
using MyMovieWatchlist.Models;

namespace MyMovieWatchlist.Services
{
    public class ExtractionService
    {
        private readonly IMoviesRepository _moviesRepository;

        private ExtractionService(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        public ExtractionService() : this(new SqlMovieRepository())
        {
        }


        public ExtractionResult Extract()
        {
            ExtractionResult result = new ExtractionResult { Movies = _moviesRepository.List() };
            return result;
        }
    }
}