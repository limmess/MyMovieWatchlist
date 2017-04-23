using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMovieWatchlist.Models
{
    public class ParseJsonToMoviesList
    {
      
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