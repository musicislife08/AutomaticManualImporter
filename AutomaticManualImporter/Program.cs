using System;
using AutomaticManualImporter.Models;
using AutomaticManualImporter.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
                    .AddLogging(config => 
                    {
                        config.AddConsole().AddDebug();
                    })
                    .AddOptions();

                    services.Configure<Settings>(hostContext.Configuration.GetSection("Settings"));
                    services.AddTransient<IRarService, RarService>()
                        .AddTransient<ISftpService, SftpService>()
                        .AddTransient<ISonarrService, SonarrService>()
                        .AddTransient<ITvService, TvService>();

                    services.AddHttpClient<ISonarrService, SonarrService>(client =>
                    {
                        client.BaseAddress = new Uri(hostContext.Configuration["Settings:SonarrUrl"] + "/api/v3/");
                        client.DefaultRequestHeaders.Add("X-Api-Key", hostContext.Configuration["Settings:SonarrApiKey"]);
                    });

                    services.AddHostedService<TvWorker>();
                    //services.AddHostedService<MovieWorker>();
                });
    }
}
