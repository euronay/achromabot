using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AchromaBot.Scraper;

public class Program
{
    private readonly IServiceProvider _services;

    public Program() => _services = CreateProvider();

    static IServiceProvider CreateProvider()
    {
        return new ServiceCollection()
            .AddLogging(config => config.AddConsole())
            .AddScraperService()
            .BuildServiceProvider();
    }

    public static Task Main(string[] args) => new Program().MainAsync();

    public async Task MainAsync()
    {
        var scraper = _services.GetRequiredService<ScraperService>();
        var logger = _services.GetRequiredService<ILogger>();
        
        var cards = await scraper.GetAllCards();

        await File.WriteAllTextAsync($"../../data/cards.json", JsonConvert.SerializeObject(cards), Encoding.Default);
    
        logger.LogInformation("Done");
    }

}
