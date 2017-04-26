using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MyMovieWatchlist.Models
{
    public class ConvertJsonMovieList
    {
        public List<Movie> Convert(List<string> jsonMovieListFromWebApi)
        {
            List<Movie> moviesView = new List<Movie>();

            foreach (var jsonMovie in jsonMovieListFromWebApi)
            {
                Movie movie = (Movie)JsonConvert.DeserializeObject(jsonMovie, typeof(Movie));
                movie.Id = ReadMoviesFromApiToMoviesList.Extract().Movies.First(m => m.imdbID == movie.imdbID).Id;
                moviesView.Add(movie);
            }
            return moviesView;
        }
    }
}