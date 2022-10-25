using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace AchromaBot.Bot;

public class EmoteService
{
    private readonly DiscordSocketClient _discordClient;
    private readonly ILogger _logger;

    public EmoteService(DiscordSocketClient discordClient, ILogger<EmoteService> logger)
    {
        _discordClient = discordClient;
        _logger = logger;
    }

    public string GetEmoteString(string emoteName)
    {
        var emote = _discordClient.Guilds.SelectMany(x => x.Emotes)
        .FirstOrDefault(x => String.Equals(x.Name, emoteName, StringComparison.OrdinalIgnoreCase));

        if (emote == null)
        {
            _logger.LogWarning($"Could not find emote :{emoteName}: on server");
            return $":{emoteName}:";
        }

        var emoteString = $"<:{emote.Name}:{emote.Id}>";

        _logger.LogDebug($"Loaded emote {emoteString}");

        return emoteString;
    }
    
}