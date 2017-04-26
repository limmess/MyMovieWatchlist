using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MyMovieWatchlist.Models
{
    public class ParseSearchResultToMoviesList
    {
        /// <summary>
        /// Converts JSON search result to movie list
        /// </summary>
        /// <param name="jsonString">JSON string (3 objects: Search, totalResults, Response)</param>
        /// <returns>Movies list</returns>
        public List<Movie> Parse(string jsonString)
        {
            //Parse to JSON object
            JObject jObject = JObject.Parse(jsonString);

            //Extract from search response movies - Object "Search"
            var MoviesInJsonFormat = jObject.SelectToken("Search").ToString();

            //Deserialize Json to movies list
            List<Movie> movies = (List<Movie>)JsonConvert.DeserializeObject(MoviesInJsonFormat, typeof(List<Movie>));

            return movies;
        }
    }
}