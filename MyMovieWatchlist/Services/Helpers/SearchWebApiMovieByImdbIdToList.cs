using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieWatchlist.Models
{

    public class SearchWebApiMovieByImdbIdToList
    {
        private readonly string _uri = ConfigurationManager.ConnectionStrings["SearchWebApiMovieByImdbId"].ConnectionString;

        /// <summary>
        /// Takes Movies imdbId's from database and gets from Web API all movies info in JSON into List
        /// </summary>
        /// <param name="movieList">List of Movies in database</param>
        /// <returns>Movies list in JSON</returns>
        public async Task<List<string>> GetValue(List<Movie> movieList)
        {
            List<string> imdbIdList = new List<string>();

            foreach (var movie in movieList)
            {
                var uri = new StringBuilder(_uri);
                var client = new HttpClient();

                //construct Web API queryy string
                var builtUri = uri.Append(movie.imdbID).ToString();

                HttpResponseMessage response = await client.GetAsync(builtUri).ConfigureAwait(continueOnCapturedContext: false);

                //check Web API response
                response.EnsureSuccessStatusCode();

                //get movie info from Web API
                string responseBody = await response.Content.ReadAsStringAsync();

                //add movie to List
                imdbIdList.Add(responseBody);

                client.Dispose();
            }

            return imdbIdList;
        }
    }
}