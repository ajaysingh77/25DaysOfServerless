using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Day4.Models
{
    public interface IPotluckPartyEntity
    {
        //IList<PotluckPartyPoco> Value { get; set; }
        //void OrganizeParty(PotluckPartyPoco partyEvent);
        //void AddOrganizer(HostPoco organizer);
        //void AddFoodToParty(int id, FoodPoco foodItem);
        //Task<IList<PotluckPartyPoco>> Get();
        //Task Reset();

        Task OrganizeParty(PotluckParty partyInfo);
        Task AddOrganizer(OrganizerInfo organizerInfo);
        Task AddFoodToParty(Guid partyId, FoodInfo foodInfo);
        Task RemoveFoodFromParty(Guid partyId, FoodInfo foodInfo);
        Task<IList<FoodInfo>> PartyFoodList(Guid partyId);
    }
}