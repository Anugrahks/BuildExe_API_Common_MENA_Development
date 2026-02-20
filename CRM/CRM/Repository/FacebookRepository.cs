using System.Net.Http;
using System.Threading.Tasks;

namespace BuildExeServices.Repository
{
    public class FacebookRepository : IFacebookRepository
    {
        private readonly HttpClient _httpClient;

        public FacebookRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAdAccountInsights(string adAccountId, string accessToken)
        {
            var response = await _httpClient.GetStringAsync($"https://graph.facebook.com/v13.0/act_{adAccountId}/insights?access_token={accessToken}");
            return response;
        }
    }
}
