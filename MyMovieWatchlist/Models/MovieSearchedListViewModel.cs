using System.Collections.Generic;

namespace MyMovieWatchlist.Models
{
    public class MovieSearchedListViewModel
    {
        public MovieSearchedListViewModel(List<Movie> movies)
        {
            Movies = movies;
        }

        public string SelectedMovieImdbId { get; set; }
        public List<Movie> Movies { get; set; }
    }
}