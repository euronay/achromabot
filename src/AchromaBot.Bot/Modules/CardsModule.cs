using AchromaBot.Bot;
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

        await ReplyAsync($"{card.Name} {card.Lore}");
    }
}