using Discord;
using Microsoft.Extensions.Logging;

namespace AchromaBot.Bot;

public class DiscordLogger
{
    private readonly ILogger _logger;

    public DiscordLogger(ILogger logger)
    {
        _logger = logger;
    }

    public Task Log(LogMessage message)
    {
        switch (message.Severity)
        {
            case LogSeverity.Critical:
                _logger.LogCritical(message.Exception, message.Message);
                break;
            case LogSeverity.Error:
                _logger.LogError(message.Exception, message.Message);
                break;
            case LogSeverity.Warning:
                _logger.LogWarning(message.Message);
                break;
            case LogSeverity.Info:
                _logger.LogInformation(message.Message);
                break;
            default:
                _logger.LogDebug(message.Message);
                break;
        }

        return Task.CompletedTask;
    }
}