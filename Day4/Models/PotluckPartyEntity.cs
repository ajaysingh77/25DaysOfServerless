using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Day4.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PotluckPartyEntity : IPotluckPartyEntity
    {
        //[JsonProperty("value")]
        //public IList<PotluckPartyModel> Value { get; set; }

        //public void Add(PotluckPartyModel partyEvent)
        //{
        //    this.Value.Add(partyEvent);
        //}

        //public Task Reset()
        //{
        //    this.Value = null;
        //    return Task.CompletedTask;
        //}

        //public Task<IList<PotluckPartyModel>> Get()
        //{
        //    return Task.FromResult(this.Value);
        //}
        //public Task<IList<PotluckPartyModel>> Get(int)
        //{
        //    return Task.FromResult(this.Value);
        //}
        //public void Delete(int )
        //{
        //    Entity.Current.DeleteState();
        //}

        [FunctionName(nameof(PotluckPartyEntity))]
        public static Task Run([EntityTrigger] IDurableEntityContext ctx)
            => ctx.DispatchAsync<PotluckPartyEntity>();

        public Task AddFoodToParty(Guid partyId, FoodInfo foodInfo)
        {
            throw new NotImplementedException();
        }

        public Task AddOrganizer(OrganizerInfo organizerInfo)
        {
            throw new NotImplementedException();
        }

        public Task OrganizeParty(PotluckParty partyInfo)
        {
            throw new NotImplementedException();
        }

        public Task<IList<FoodInfo>> PartyFoodList(Guid partyId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFoodFromParty(Guid partyId, FoodInfo foodInfo)
        {
            throw new NotImplementedException();
        }
    }
}
