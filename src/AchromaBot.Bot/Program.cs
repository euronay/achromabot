using AchromaBot.Scraper;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AchromaBot.Bot;

public class Program
{
    private readonly IServiceProvider _services;

    public Program()
    {
        _services = CreateProvider();
    }

    static IServiceProvider CreateProvider()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile($"appsettings.json",true,true)
            .AddEnvironmentVariables()
            .Build();

        var appSettings = config.GetSection("App").Get<AppSettings>();

        return new ServiceCollection()
            .AddLogging(config => config.AddConsole())
            .AddSingleton(appSettings)          
            .AddDiscordServices()
            .AddAchromaBotServices()
            .AddScraperService()  
            .BuildServiceProvider();
    }

    public static Task Main(string[] args) => new Program().MainAsync();

    public async Task MainAsync()
    {
        var startupService = _services.GetRequiredService<StartupService>();   
        await startupService.Run();
    }

}
