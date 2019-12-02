using DaysOfServerless.Day1.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(DaysOfServerless.Day1.Startup))]

namespace DaysOfServerless.Day1
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            var configBuilder = new ConfigurationBuilder();

            string scriptRootPath = Environment.GetEnvironmentVariable("AzureWebJobsScriptRoot");

            if (!string.IsNullOrEmpty(scriptRootPath))
            {
                configBuilder.SetBasePath(scriptRootPath).AddJsonFile("local.settings.json", optional: false, reloadOnChange: false);
            }

            configBuilder.AddEnvironmentVariables();

            var configuration = configBuilder.Build();

            builder.Services.Configure<DreidelOptions>((settings) =>
            {
                var dreidels = configuration.GetSection("Dreidels");
                settings.Dreidels = dreidels.Value.Split(',');
            });

            builder.Services.AddSingleton<ISpinDreidel, SpinDreidelService>();

        }
    }
}