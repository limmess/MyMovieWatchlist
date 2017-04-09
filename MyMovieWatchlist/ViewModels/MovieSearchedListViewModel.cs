using MyMovieWatchlist.Models;
using System.Collections.Generic;

namespace MyMovieWatchlist.ViewModels
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