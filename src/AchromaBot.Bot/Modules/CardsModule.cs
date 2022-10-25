using System.Text;
using AchromaBot.Bot;
using AchromaBot.Common;
using Discord;
using Discord.Commands;

public class CardsModule : ModuleBase<SocketCommandContext>
{
    private Lazy<string> shardRed;
    private Lazy<string>  shardYellow;
    private Lazy<string>  shardGreen;
    private Lazy<string>  shardCyan;
    private Lazy<string>  shardBlue;
    private Lazy<string>  shardMagenta;
    private Lazy<string>  shardBlack;

    private readonly CardService _cardService;

    public CardsModule(CardService cardService, EmoteService emoteService)
    {
        _cardService = cardService;

        shardRed = new Lazy<string>(() => emoteService.GetEmoteString("shard_red"));
        shardYellow = new Lazy<string>(() => emoteService.GetEmoteString("shard_yellow"));
        shardGreen = new Lazy<string>(() => emoteService.GetEmoteString("shard_green"));
        shardCyan = new Lazy<string>(() => emoteService.GetEmoteString("shard_cyan"));
        shardBlue = new Lazy<string>(() => emoteService.GetEmoteString("shard_blue"));
        shardMagenta = new Lazy<string>(() => emoteService.GetEmoteString("shard_magenta"));
        shardBlack = new Lazy<string>(() => emoteService.GetEmoteString("shard_black"));

    }

    [Command("say")]
    public Task Say([Remainder]string text) => ReplyAsync(text);

    [Command("c")]
    public async Task Card([Remainder]string query)
    {
        var card = _cardService.GetSingleCard(query);

        if(card == null)
        {
            var error = new EmbedBuilder()
                .WithDescription($"Sorry I couldn't find a card named '{query}'")
                .WithColor(Color.Red)
                .Build();

            await ReplyAsync(embed: error);
        }

        var imageUrl = $"https://storage.googleapis.com/achroma-697e8-cards/{card.CardId.Split("/").First()}.png";

        var url = $"https://explore.achroma.cards/{card.Id}";

        var embed = new EmbedBuilder()
            .WithTitle(card.Name)
            .WithUrl(url)
            .WithThumbnailUrl(imageUrl)
            .WithDescription(GetTypeString(card))
            .WithColor(Color.Blue)
            .AddField("Effect", GetEffectString(card), true)
            .AddField("Shard Value", GetShardValue(card), true)
            .AddField("Gameplay", card.GamePlay.DashIfEmpty())
            .AddField("Lore", $"*{card.Lore.DashIfEmpty()}*")
            .Build();

        await ReplyAsync(embed: embed);
    }

    public string GetTypeString(AchromaCard card) => 
        $"{(card.Heroic == "Is heroic" ? "Heroic " : "")}{card.Type}{(card.Subtypes.Any()? " - ": "")}{String.Join(" ", card.Subtypes)}";

    private string GetEffectString(AchromaCard card)
        => card.EffectValue > 0 ? $"{card.EffectType} {card.EffectValue}" : "-";

    private string GetShardValue(AchromaCard card)
        => $"{card.ShardValue} {Repeat(shardRed.Value, card.ShardRed)}{Repeat(shardYellow.Value, card.ShardYellow)}{Repeat(shardGreen.Value, card.ShardGreen)}{Repeat(shardCyan.Value, card.ShardCyan)}{Repeat(shardBlue.Value, card.ShardBlue)}{Repeat(shardMagenta.Value, card.ShardMagenta)}{Repeat(shardBlack.Value, card.ShardBlack)}";

    private static string Repeat(string s, int? count) => new StringBuilder().Insert(0, s, count ?? 0).ToString();
}