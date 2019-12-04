using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Day3.Models;

namespace Webhook
{
    public class GitHubWebhook
    {
        /// <summary>
        /// This functions Stored the Url of any PNG file Table Storage commited in GitHub  
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("GitHubWebhookForPNGFilePush")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Validate the GitHub Event Type
            if (req.Headers.ContainsKey("X-GitHub-Event"))
            {
                if (req.Headers["X-GitHub-Event"] != "push") return new OkResult();
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation(requestBody);
            GitHub.WebHook.PushEvent.Rootobject imageData = JsonConvert.DeserializeObject<GitHub.WebHook.PushEvent.Rootobject>(requestBody);
            // Get Storage Reference 
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(System.Environment.GetEnvironmentVariable("AzureWebJobsStorage"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("GitHubPngUrls");
            await table.CreateIfNotExistsAsync();

            foreach (var item in imageData.commits)
            {
                foreach (var fileName in item.added)
                {

                    if (fileName.ToLower().Contains(".png"))
                    {
                        string fileNameToSave = string.Empty;
                        if (fileName.Contains("/")) { fileNameToSave = fileName.Substring(fileName.IndexOf("/") + 1); }

                        ImageEntity imageEntity = new ImageEntity(fileNameToSave)
                        {
                            ImageUrl = $"{imageData.repository.url}/{fileName}"
                        };

                        TableOperation tableOperation = TableOperation.Insert(imageEntity);
                        await table.ExecuteAsync(tableOperation);
                    }
                }
            }


            log.LogInformation(JsonConvert.SerializeObject(imageData));
            return new OkResult();
        }
    }
}
