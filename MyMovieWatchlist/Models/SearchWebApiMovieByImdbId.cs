using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieWatchlist.Models
{
    public class SearchWebApiMovieByImdbId
    {
        private readonly string _uri = ConfigurationManager.ConnectionStrings["SearchWebApiMovieByImdbId"].ConnectionString;

        public async Task<string> GetValue(string imdbId)
        {
            var uri = new StringBuilder(_uri);
            var builtUri = uri.Append(imdbId).ToString();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(builtUri).ConfigureAwait(continueOnCapturedContext: false);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}