namespace MyMovieWatchlist.ViewModels
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {

        }
        public ErrorViewModel(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
    }
}