using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            .Build();

        var appSettings = config.GetSection("App").Get<AppSettings>();

        return new ServiceCollection()
            .AddSingleton(appSettings)
            .AddSingleton<DiscordSocketClient>(_ => new DiscordSocketClient(new DiscordSocketConfig(){
                GatewayIntents = GatewayIntents.AllUnprivileged
                | GatewayIntents.MessageContent 
                }))
            .AddSingleton<CommandService>(_ => new CommandService(new CommandServiceConfig()))
            .AddSingleton<CommandHandler>()
            .AddSingleton<CardService>()
            .BuildServiceProvider();
    }

    public static Task Main(string[] args) => new Program().MainAsync();

    public async Task MainAsync()
    {
        var settings = _services.GetRequiredService<AppSettings>();   
        var client = _services.GetRequiredService<DiscordSocketClient>();
        client.Log += Log;

        await client.LoginAsync(TokenType.Bot, settings.Token);
        await client.StartAsync();

        var commandHandler = _services.GetRequiredService<CommandHandler>();

        await commandHandler.InstallCommandsAsync();

        await Task.Delay(-1);
    }

    private Task Log(LogMessage message)
    {
        Console.WriteLine(message.ToString());
        return Task.CompletedTask;
    }

}
