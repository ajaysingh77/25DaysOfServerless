using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Day4.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class OrganizerEntity : IOrganizerEntity
    {
        [JsonProperty]
        public string OrganizerId;

        // Party Location
        [JsonProperty]
        public string Location { get; private set; }

        [JsonProperty]
        public IList<FoodInfo> FoodInfos { get; private set; }

        [FunctionName(nameof(OrganizerEntity))]
        public static Task Run([EntityTrigger] IDurableEntityContext context)
        {
            // we initialize the entity explicitly here (instead of relying on the implicit default constructor)
            // so we can set the user id to match the entity key
            if (!context.HasState)
            {

                context.SetState(new OrganizerEntity()
                {
                    OrganizerId = context.EntityKey,
                });
            }

            return context.DispatchAsync<OrganizerEntity>();
        }

        public Task SetLocation(string location)
        {
            this.Location = location;

            return Task.CompletedTask;
        }

        public Task<OrganizerEntity> GetState()
        {
            return Task.FromResult(this);
        }

        public void AddFoodInfo(FoodInfo foodInfo)
        {
            if (FoodInfos == null)
            {
                List<FoodInfo> foodInfos = new List<FoodInfo>
                {
                    foodInfo
                };
                FoodInfos = foodInfos;
            }
            else
            {
                var item = FoodInfos.FirstOrDefault(x => x.Name == foodInfo.Name && x.Owner == foodInfo.Owner);
                if (item == null)
                {
                    FoodInfos.Add(foodInfo);
                }
            }
        }

        public void RemoveFoodInfo(FoodInfo foodInfo)
        {
            if (FoodInfos != null)
            {
                var itemToRemove = FoodInfos.FirstOrDefault(x => x.Name == foodInfo.Name && x.Owner == foodInfo.Owner);
                FoodInfos.Remove(itemToRemove);
            }
        }

        public Task<IList<FoodInfo>> GetAllFoodInfo()
        {
            return Task.FromResult(FoodInfos);
        }
    }
}
