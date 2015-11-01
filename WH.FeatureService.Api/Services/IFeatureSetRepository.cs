using System.Threading.Tasks;
using WH.FeatureService.Api.Models;

namespace WH.FeatureService.Api.Services
{
    public interface IFeatureSetRepository
    {
        Task<string> GetLatestVersion(int orgId, string deviceVersion);
        Task<FeatureSet> GetSet(int orgId, string deviceVersion, int clientId);
    }
}