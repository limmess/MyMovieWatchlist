using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyMovieWatchlist.Models
{
    public class SearchWebApiMoviesByName
    {
        private readonly string _uri = ConfigurationManager.ConnectionStrings["SearchWebApiMoviesByName"].ConnectionString;

        public async Task<string> GetValue()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_uri).ConfigureAwait(continueOnCapturedContext: false);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            client.Dispose();
            return responseBody;
        }
    }
}