using System.Text;
using AchromaBot.Common;

namespace AchromaBot.Bot;

public static class AchromaCardExtensions
{
    private const string shardRed = "<:shard_red:1027171557195403326>";
    private const string shardYellow = "<:shard_yellow:1027171555765141514>";
    private const string shardGreen = "<:shard_green:1027171554397782026>";
    private const string shardCyan = "<:shard_cyan:1027171552627785739>";
    private const string shardBlue = "<:shard_blue:1027171551424020571>";
    private const string shardMagenta = "<:shard_magenta:1027171550144778310>";
    private const string shardBlack = "<:shard_black:1027171477142900756>";

    public static string FormatShardValue(this AchromaCard card) => 
        $"{card.ShardValue} {Repeat(shardRed, card.ShardRed)}{Repeat(shardYellow, card.ShardYellow)}{Repeat(shardGreen, card.ShardGreen)}{Repeat(shardCyan, card.ShardCyan)}{Repeat(shardBlue, card.ShardBlue)}{Repeat(shardMagenta, card.ShardMagenta)}{Repeat(shardBlack, card.ShardBlack)}";

    private static string Repeat(string s, int? count) => new StringBuilder().Insert(0, s, count ?? 0).ToString();

    public static string FormatType(this AchromaCard card) => 
        $"{(card.Heroic == "Is heroic" ? "Heroic " : "")}{card.Type}{(card.Subtypes.Any()? " - ": "")}{String.Join(" ", card.Subtypes)}";
}