
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        var discordConfig = new DiscordSocketConfig(){};

        var commandConfig = new CommandServiceConfig(){};

        return new ServiceCollection()
            .AddSingleton(appSettings)
            .AddSingleton(discordConfig)
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton(commandConfig)
            .AddSingleton<CommandService>()
            .AddSingleton<CommandHandler>()
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
