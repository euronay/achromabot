using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace AchromaBot.Bot;

public static class DependencyInjection
{
    public static IServiceCollection AddAchromaBotServices(this IServiceCollection services) => services
            .AddSingleton<CommandHandler>()
            .AddSingleton<CardService>()
            .AddSingleton<EmoteService>()
            .AddSingleton<StartupService>();

    public static IServiceCollection AddDiscordServices(this IServiceCollection services) => services
            .AddSingleton<DiscordSocketClient>(_ => new DiscordSocketClient(new DiscordSocketConfig(){
                GatewayIntents = GatewayIntents.AllUnprivileged
                    | GatewayIntents.MessageContent 
                    | GatewayIntents.Guilds 
                    | GatewayIntents.GuildEmojis
                }))
            .AddSingleton<CommandService>(_ => new CommandService(new CommandServiceConfig()));
}