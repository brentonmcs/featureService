using System;
using WH.FeatureService.Api.Models;
using WH.FeatureService.Api.Common;
using Microsoft.Framework.Logging;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace WH.FeatureService.Api.Services
{
    public class FeatureSetRepository : IFeatureSetRepository
    {
        private readonly ICache _cache;
        private readonly ILogger _logger;
        private readonly IMongoConnector _mongoConnector;

        public FeatureSetRepository(ICache cache, ILogger<FeatureSetRepository> logger, IMongoConnector mongoConnector)
        {
            _cache = cache;
            _logger = logger;
            _mongoConnector = mongoConnector;
        }
        public async Task<Guid> GetLatestVersion(int orgId, string deviceVersion)
        {
            var latestVersion = _cache.Get<Guid>("Version-" + orgId + "-" + deviceVersion);

            if (latestVersion == default(Guid))
            {
                var filterOrg = Builders<Models.Version>.Filter.Eq("OrgId", orgId);
                var filterDevice = Builders<Models.Version>.Filter.Eq("DeviceVersion", deviceVersion); 
                var filter = Builders<Models.Version>.Filter.And(filterOrg,filterDevice);
                var mongoSet = await _mongoConnector.QueryAsync("Version", filter);
                latestVersion = mongoSet.Id;
            }

            _logger.LogInformation(latestVersion.ToString());
            return latestVersion;
        }

        public async Task<FeatureSet> GetSet(int orgId, string deviceVersion, int clientId)
        {
            var latest = await GetLatestVersion(orgId, deviceVersion);

            var featureSetKey = "FeatureSet-" + latest.ToString();
            var cachedSet = _cache.Get<FeatureSet>(featureSetKey);
            if (cachedSet != null)
            {
                return cachedSet;
            }

            var mongoSet = await _mongoConnector.QueryAsync("FeatureSet", Builders<FeatureSet>.Filter.Eq("Version", latest));
            _cache.Set(featureSetKey, mongoSet);
            return mongoSet;
        }

    }
}
