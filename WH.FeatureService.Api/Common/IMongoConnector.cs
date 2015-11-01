using System.Threading.Tasks;
using MongoDB.Driver;

namespace WH.FeatureService.Api.Common
{
	public interface IMongoConnector
	{
		Task<T> QueryAsync<T>(string collection, FilterDefinition<T> filter);
	}
}
	