using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using DaysOfServerless.Day1.Services;

namespace DaysOfServerless.Day1
{
    public class SpinDreidel
    {
        readonly ISpinDreidel _spinDreidel;

        public SpinDreidel(ISpinDreidel service)
        {
            _spinDreidel = service;
        }

        [FunctionName("SpinDreidel")]
        public async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "dreidel/spin")] HttpRequest req, ILogger log)
        {
            log.LogInformation("Fetching random dreidel value");
            var dreidel = await _spinDreidel.Spin();
            return new OkObjectResult(dreidel);
        }
    }
}
