using MyMovieWatchlist.Models;
using MyMovieWatchlist.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MyMovieWatchlist.Impl
{
    public class ConvertJsonToMovieList
    {
        private readonly DatabaseService _myDatabaseService = new DatabaseService();

        public IEnumerable<Movie> Convert(IEnumerable<string> jsonMovieListFromWebApi)
        {
            List<Movie> moviesView = new List<Movie>();

            foreach (string jsonMovie in jsonMovieListFromWebApi)
            {
                Movie movie = (Movie)JsonConvert.DeserializeObject(jsonMovie, typeof(Movie));
                movie.Id = _myDatabaseService.ReadAllMoviesFromDatabase().First(m => m.imdbID == movie.imdbID).Id;
                moviesView.Add(movie);
            }
            return moviesView;
        }
    }
}