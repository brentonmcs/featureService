using System;

namespace WH.FeatureService.Api.Models
{
	public class Version
    {
        public string OrgId {get;set;}
        
        public string DeviceVersion {get;set;}
        
        public Guid Id { get; set; }
    }
}