using System.Threading.Tasks;
using MongoDB.Driver;

namespace WH.FeatureService.Api.Common
{
    public class MongoConnector : IMongoConnector
    {
        string databaseName = "FeatureDb";

        public async Task<T> QueryAsync<T>(string collection, FilterDefinition<T> filter)
        {
            var col = new MongoClient("mongodb://localhost:27017").GetDatabase(databaseName).GetCollection<T>(collection);
            return await col.Find(filter).FirstOrDefaultAsync();
        }
			
    }
}