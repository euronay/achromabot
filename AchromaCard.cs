using Newtonsoft.Json;

public class AchromaCard
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("cardId")]
    public string CardId { get; set; }

    [JsonProperty("effectType")]
    public string EffectType { get; set; }

    [JsonProperty("effectValue")]
    public int? EffectValue { get; set; }

    [JsonProperty("gamePlay")]
    public string GamePlay { get; set; }

    [JsonProperty("heroic")]
    public string Heroic { get; set; }

    [JsonProperty("lore")]
    public string Lore { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("objectID")]
    public string ObjectId { get; set; }

    [JsonProperty("palettes")]
    public List<string> Palettes { get; set; }

    [JsonProperty("printDate")]
    public DateTime? PrintDate { get; set; }

    [JsonProperty("printDate_timestamp")]
    public int? PrintDateTimestamp { get; set; }

    [JsonProperty("rarity")]
    public string Rarity { get; set; }

    [JsonProperty("raritySortValue")]
    public int? RaritySortValue { get; set; }

    [JsonProperty("realm")]
    public string Realm { get; set; }

    [JsonProperty("set")]
    public string Set { get; set; }

    [JsonProperty("shardBlack")]
    public int? ShardBlack { get; set; }

    [JsonProperty("shardBlue")]
    public int? ShardBlue { get; set; }

    [JsonProperty("shardCyan")]
    public int? ShardCyan { get; set; }

    [JsonProperty("shardGreen")]
    public int? ShardGreen { get; set; }

    [JsonProperty("shardMagenta")]
    public int? ShardMagenta { get; set; }

    [JsonProperty("shardRed")]
    public int? ShardRed { get; set; }

    [JsonProperty("shardValue")]
    public int? ShardValue { get; set; }

    [JsonProperty("shardYellow")]
    public int? ShardYellow { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("subtypes")]
    public List<string> Subtypes { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
}