using AchromaBot.Bot;
using Discord;
using Discord.Commands;

public class CardsModule : ModuleBase<SocketCommandContext>
{
    private readonly CardService _cardService;

    public CardsModule(CardService cardService)
    {
        _cardService = cardService;
    }

    [Command("say")]
    public Task Say([Remainder]string text) => ReplyAsync(text);

    [Command("c")]
    public async Task Card([Remainder]string query)
    {
        var card = await _cardService.GetSingleCardAsync(query);

        if(card == null)
        {
            await ReplyAsync($"Could not find '{query}'");
        }

        var imageUrl = $"https://storage.googleapis.com/achroma-697e8-cards/{card.CardId.Split("/").First()}.png";

        var url = $"https://explore.achroma.cards/{card.Id}";

        var embed = new EmbedBuilder()
            .WithTitle(card.Name)
            .WithUrl(url)
            .WithThumbnailUrl(imageUrl)
            .WithDescription(card.FormatType())
            .AddField("Effect", $"{card.EffectType} {card.EffectValue}", true)
            .AddField("Shard Value", card.FormatShardValue(), true)
            .AddField("Gameplay", card.GamePlay)
            .AddField("Lore", $"*{card.Lore}*")
            .Build();

        await ReplyAsync(embed: embed);
    }
}