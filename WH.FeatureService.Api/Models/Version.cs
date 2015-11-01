
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WH.FeatureService.Api.Models
{
    public class Version
    {
        public int OrgId { get; set; }

        public string DeviceVersion { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}