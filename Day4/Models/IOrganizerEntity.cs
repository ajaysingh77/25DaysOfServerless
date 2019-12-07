using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Day4.Models
{
    public interface IOrganizerEntity
    {
        void AddFoodInfo(FoodInfo foodInfo);
        void RemoveFoodInfo(FoodInfo foodInfo);
        Task SetLocation(string location);
        //Task<OrganizerInfo> GetOrganizer(Guid organizerId);
        Task<IList<FoodInfo>> GetAllFoodInfo();
        Task<OrganizerEntity> GetState();
    }
}