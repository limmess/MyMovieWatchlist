using MyMovieWatchlist.Services;

namespace MyMovieWatchlist.Models
{
    public class ReadMoviesFromApiToMoviesList
    {
        private static ExtractionResult _extractionResult;
        private readonly ConvertJsonMovieList _convertJsonMovieList = new ConvertJsonMovieList();

        public static ExtractionResult Extract()
        {
            var myService = new ExtractionService();
            _extractionResult = myService.Extract();
            return _extractionResult;
        }
    }
}