using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Day4.Models
{
    public class PotluckPartyInfo
    {
        [JsonProperty]
        public string PartyId { get; set; }
        [JsonProperty]
        public string OrganizerId { get; set; }
        [JsonProperty]
        public IList<FoodInfo> FoodList { get; set; }

    }

    [JsonObject(MemberSerialization.OptOut)]
    public class OrganizerInfo
    {

        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public string Location { get; set; }
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class FoodInfo
    {
        public string Owner { get; set; }
        public string Name { get; set; }
    }


}
