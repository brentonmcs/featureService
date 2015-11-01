
using System;

namespace WH.FeatureService.Api.Models
{
    public class FeatureSetRequest
    {
        public int OrgId { get; set; }

        public string DeviceVersion { get; set; }

        public Guid KnownVersion { get; set; }

        public int ClientId { get; set; }

    }
}
