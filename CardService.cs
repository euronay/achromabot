
using GraphQL.Client.Http;
using Newtonsoft.Json;
using System.Linq;

public class CardService
{
    private readonly GraphQLHttpClient _client;

    private class SearchQuery
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("items")]
        public AchromaCard[] Items { get; set; }
    }

    private class CardResponse
    {
        [JsonProperty("searchQuery")]
        public SearchQuery SearchQuery { get; set; }
    }

    public CardService(GraphQLHttpClient client)
    {
        this._client = client;
    }

    public async Task<AchromaCard> GetSingleCard(string query)
    {
        var cardRequest = new GraphQLHttpRequest
        {
            Query = @"query search($indexName: String, $query: String, $perPage: Int, $page: Int, $facetFilters: [[String]]) {
  searchQuery(
    indexName: $indexName
    query: $query
    perPage: $perPage
    page: $page
    facetFilters: $facetFilters
  ) {
    id
    items {
      id
      cardId
      effectType
      effectValue
      gamePlay
      heroic
      lore
      name
      objectID
      palettes
      printDate
      printDate_timestamp
      rarity
      raritySortValue
      realm
      set
      shardBlack
      shardBlue
      shardCyan
      shardGreen
      shardMagenta
      shardRed
      shardValue
      shardYellow
      status
      subtypes
      type
    }
  }
}",
            OperationName = "search",
            Variables = new {
                indexName = "cards.date_desc",
                query = query,
                page = 1
            }
        };

        try
        {
            var response = await _client.SendQueryAsync<CardResponse>(cardRequest);

            return response.Data.SearchQuery.Items.GetClosestMatch(query);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.ToString());
            return null;
        }
    }

}
