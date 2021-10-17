using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teams.Application.Responses
{



    public class Value
    {
        public string id { get; set; }
        public object createdDateTime { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public object internalId { get; set; }
        public object classification { get; set; }
        public object specialization { get; set; }
        public object visibility { get; set; }
        public object webUrl { get; set; }
        public bool isArchived { get; set; }
        public object isMembershipLimitedToOwners { get; set; }
        public object memberSettings { get; set; }
        public object guestSettings { get; set; }
        public object messagingSettings { get; set; }
        public object funSettings { get; set; }
        public object discoverySettings { get; set; }
    }

    public class AllTeams
    {
       

        [JsonProperty("@odata.count")]
        public int OdataCount { get; set; }
        public List<Value> value { get; set; }
    }




}
