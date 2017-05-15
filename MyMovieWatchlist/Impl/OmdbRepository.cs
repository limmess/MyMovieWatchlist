using MyMovieWatchlist.Interfaces;
using MyMovieWatchlist.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieWatchlist.Impl
{
    public class OmdbRepository : IWebApiRepository
    {
        private readonly string _uriByImdbId = ConfigurationManager.ConnectionStrings["SearchWebApiMovieByImdbId"].ConnectionString;
        private readonly string _uriByName = ConfigurationManager.ConnectionStrings["SearchWebApiMoviesByName"].ConnectionString;
        private readonly HttpClient _client = new HttpClient();

        public Task<string> SearchMovieByImdbId(string imdbId)
        {
            string builtUri = BuildSearchStringImdbId(imdbId);
            Task<string> result = QueryWebApi(builtUri);
            return result;
        }

        /// <summary>
        /// Search Movie By Name
        /// </summary>
        /// <param name="movieName">Search string</param>
        /// <returns>Result in JSON. Success=(3 objects: Search, totalResults, Response). Error=(2 objects: Response, Error)</returns>
        public Task<string> SearchMovieByName(string movieName)
        {
            string builtUri = BuildSearchStringMovieName(movieName);
            Task<string> result = QueryWebApi(builtUri);
            return result;
        }

        public IEnumerable<string> AddMoviesInfo(IEnumerable<Movie> moviesFromDatabaseList)
        {
            List<string> moviesWithFullInfoJsonList = new List<string>();

            foreach (Movie movie in moviesFromDatabaseList)
            {
                string builtUri = BuildSearchStringImdbId(movie.imdbID);
                Task<string> result = QueryWebApi(builtUri);
                moviesWithFullInfoJsonList.Add(result.Result);
            }

            return moviesWithFullInfoJsonList;
        }

        public bool ResponseIsValid(string searchResponseInJson)
        {
            // Parse to JSON object
            JObject jObject = JObject.Parse(searchResponseInJson);

            //Extract from search response movies - Object "Response"
            string response = jObject.SelectToken("Response").ToString();

            //Convert string to Bool
            bool boolVal = Convert.ToBoolean(response);

            return boolVal;
        }


        #region Helpers

        private async Task<string> QueryWebApi(string builtUri)
        {
            HttpResponseMessage response = await _client.GetAsync(builtUri).ConfigureAwait(continueOnCapturedContext: false);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        private string BuildSearchStringImdbId(string imdbId)
        {
            StringBuilder _uriByImdbId = new StringBuilder(this._uriByImdbId);
            string uri = _uriByImdbId.Append(imdbId).ToString();
            return uri;
        }

        private string BuildSearchStringMovieName(string movieName)
        {
            movieName = movieName.Trim().Replace(' ', '+');
            StringBuilder _uriByName = new StringBuilder(this._uriByName);
            string uri = _uriByName.Append(movieName).ToString();
            return uri;
        }



        #endregion Helpers
    }
}