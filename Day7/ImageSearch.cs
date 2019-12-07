using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Azure.CognitiveServices.Search.ImageSearch;
using Microsoft.Azure.CognitiveServices.Search.ImageSearch.Models;
using System.Linq;

namespace Day7
{
    public static class ImageSearch
    {
        [FunctionName("ImageSearch")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "searchimage/{imageTag}")] HttpRequest req,
            ILogger log,
            string imageTag)
        {
            var result = await SearchImage(imageTag).ConfigureAwait(false);
            return new RedirectResult(result, false);
        }

        public static async Task<string> SearchImage(string imageTag)
        {
            var client = new ImageSearchClient(new ApiKeyServiceClientCredentials(Environment.GetEnvironmentVariable("BingSearch_Access_Key")));

            //stores the image results returned by Bing
            // make the search request to the Bing Image API, and get the results"
            Images imageResults = await client.Images.SearchAsync(query: imageTag); //search query
            //display the details for the first image result.
            var firstImageResult = imageResults.Value.First();

            return firstImageResult.ThumbnailUrl;

        }
    }

}
