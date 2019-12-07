using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Net.Http;
using Day4.Models;
using System.Net;
using System.Collections.Generic;

namespace Day4
{
    public static class Function1
    {

        [FunctionName("AddFoodInfoToPartyPost")]
        public static async Task<HttpResponseMessage> OrganizerInfoGet(
            [HttpTrigger(AuthorizationLevel.Function, "Post", Route = "organizer/{organizerId}")] HttpRequestMessage req,
            [DurableClient] IDurableClient client,
            ILogger log,
            string organizerId)
        {
            var target = new EntityId(nameof(OrganizerEntity), organizerId);

            string requestBody = await req.Content.ReadAsStringAsync();
            FoodInfo foodInfo = JsonConvert.DeserializeObject<FoodInfo>(requestBody);

            await client.SignalEntityAsync<IOrganizerEntity>(target, proxy => proxy.AddFoodInfo(foodInfo));

            return req.CreateResponse(HttpStatusCode.Accepted);
        }

        [FunctionName("GetFoodInfo")]
        public static async Task<HttpResponseMessage> GetFoodInfo(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "organizer/{organizerId}")] HttpRequestMessage req,
            [DurableClient] IDurableClient client,
            ILogger log,
            string organizerId)
        {
            var target = new EntityId(nameof(OrganizerEntity), organizerId);
            var entityState = await client.ReadEntityStateAsync<OrganizerEntity>(target);
            if (entityState.EntityExists)
            {
                return req.CreateResponse(HttpStatusCode.OK, entityState.EntityState.FoodInfos);
            }
            {
                return req.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [FunctionName("RemoveFoodInfo")]
        public static async Task<HttpResponseMessage> RemoveFoodInfo(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "organizer/{organizerId}")] HttpRequestMessage req,
            [DurableClient] IDurableClient client,
            ILogger log,
            string organizerId)
        {
            var target = new EntityId(nameof(OrganizerEntity), organizerId);
            string requestBody = await req.Content.ReadAsStringAsync();
            FoodInfo foodInfo = JsonConvert.DeserializeObject<FoodInfo>(requestBody);

            await client.SignalEntityAsync<IOrganizerEntity>(target, proxy => proxy.RemoveFoodInfo(foodInfo));

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
