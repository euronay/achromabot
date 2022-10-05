
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AchromaBot.Common;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AchromaBot.Scraper;

public class Program
{
    private readonly IServiceProvider _services;

    public Program() => _services = CreateProvider();

    static IServiceProvider CreateProvider()
    {
        return new ServiceCollection()
            .AddScraperService()
            .BuildServiceProvider();
    }

    public static Task Main(string[] args) => new Program().MainAsync();

    public async Task MainAsync()
    {
        var scraper = _services.GetRequiredService<ScraperService>();

        foreach(var set in Enum.GetValues<AchromaSet>())
        {
            Console.WriteLine($"Getting {set.GetDescription()}");

            var cards = await scraper.GetCardsForSet(set);

            await File.WriteAllTextAsync($"../../data/{set}.json", JsonConvert.SerializeObject(cards), Encoding.Default);
        }

        Console.WriteLine("Done");
    }

}
