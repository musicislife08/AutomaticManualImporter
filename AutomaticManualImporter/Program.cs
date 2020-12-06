using System;
using AutomaticManualImporter.Models;
using AutomaticManualImporter.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutomaticManualImporter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services
                    .AddLogging()
                    .AddOptions()
                    .AddHttpClient();

                    services.AddHttpClient<ISonarrService, SonarrService>(client =>
                    {
                        client.BaseAddress = new Uri(hostContext.Configuration["SonarrUrl"] + "/api/v3/");
                        client.DefaultRequestHeaders.Add("X-Api-Key", hostContext.Configuration["SonarrApiKey"]);
                    });

                    services.AddHostedService<Worker>();
                });
    }
}
