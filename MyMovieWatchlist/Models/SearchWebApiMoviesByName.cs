using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieWatchlist.Models
{
    public class SearchWebApiMoviesByName
    {
        private readonly string _uri = ConfigurationManager.ConnectionStrings["SearchWebApiMoviesByName"].ConnectionString;

        public async Task<string> GetValue(string search)
        {
            search = search.Replace(' ', '+');
            var _uri = new StringBuilder(this._uri);
            var uri = _uri.Append(search).ToString();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(continueOnCapturedContext: false);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            client.Dispose();
            return responseBody;
        }
    }
}