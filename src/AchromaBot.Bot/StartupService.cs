using AchromaBot.Scraper;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace AchromaBot.Bot;

public class StartupService
{
    private readonly AppSettings _appSettings;
    private readonly ScraperService _scraper;
    private readonly CardService _cardService;
    private readonly DiscordSocketClient _discordClient;
    private readonly CommandHandler _commandHandler;
    private readonly ILogger _logger;

    public StartupService(AppSettings appSettings,
                        ScraperService scraper,
                        CardService cardService,
                        DiscordSocketClient discordClient,
                        CommandHandler commandHandler,
                        ILogger<StartupService> logger)
    {
        _appSettings = appSettings;
        _scraper = scraper;
        _cardService = cardService;
        _discordClient = discordClient;
        _commandHandler = commandHandler;
        _logger = logger;
    }

    public async Task Run()
    {
        _logger.LogInformation("Starting AchromaBot");

        var cards = await _scraper.GetAllCards();
        _cardService.InitCards(cards);

        var discordLogger = new DiscordLogger(_logger);
        _discordClient.Log += discordLogger.Log;

        await _discordClient.LoginAsync(TokenType.Bot, _appSettings.Token);
        await _discordClient.StartAsync();

        await _commandHandler.InstallCommandsAsync();

        await Task.Delay(-1);
    }
}