using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WH.FeatureService.Api.Models;
using WH.FeatureService.Api.Services;

namespace WH.FeatureService.Api.Controllers
{
    [Route("api/[controller]")]
    public class FeatureSetController : Controller
    {
        private readonly IFeatureSetRepository _featureSetRepository;

        public FeatureSetController(IFeatureSetRepository featureSetRepository)
        {
            _featureSetRepository = featureSetRepository;
        }

        [HttpGet, ResponseCache(Duration = 30)]
        public async Task<IActionResult> Get(FeatureSetRequest featureSetRequest)
        {
            if (featureSetRequest.KnownVersion == await _featureSetRepository.GetLatestVersion(featureSetRequest.OrgId, featureSetRequest.DeviceVersion))
            {
                return new HttpStatusCodeResult(304);
            }
            return Ok(await _featureSetRepository.GetSet(featureSetRequest.OrgId, featureSetRequest.DeviceVersion, featureSetRequest.ClientId));
        }
    }
}
