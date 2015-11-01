using System.Collections.Generic;
using System.Linq;
using WH.FeatureService.Api.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WH.FeatureService.Api.Models
{
    public class FeatureSet
    {
        [BsonRepresentation(BsonType.ObjectId)] 
        public string Id { get; set; }
        public string Version { get; set; }

        public List<Feature> Features { get; set; }

        public string CheckSum
        {
            get
            {
                if (Features == null || !Features.Any())
                {
                    return string.Empty;
                }

                var featureMerged = string.Empty;
                foreach (var item in Features)
                {
                    featureMerged += item.Name + item.Value;
                }

                return CheckSumHelper.GenerateCheckSum(featureMerged);
            }
        }
    }
}
