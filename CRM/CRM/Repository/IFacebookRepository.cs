using System.Threading.Tasks;

namespace BuildExeServices.Repository
{
    public interface IFacebookRepository
    {
        Task<string> GetAdAccountInsights(string adAccountId, string accessToken);
    }
}
