using Discord.Commands;

public class CardsModule : ModuleBase<SocketCommandContext>
{
    [Command("hello")]
    public Task Say([Remainder]string text) => ReplyAsync($"Hello '{text}'");
}