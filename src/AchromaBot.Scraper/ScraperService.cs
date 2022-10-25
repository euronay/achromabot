
using AchromaBot.Common;
using GraphQL.Client.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AchromaBot.Scraper;

public class ScraperService
{
    private readonly GraphQLHttpClient _client;
    private readonly ILogger _logger;

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

    public ScraperService(GraphQLHttpClient client, ILogger<ScraperService> logger)
    {
        this._client = client;
        _logger = logger;
    }

    public async Task<IEnumerable<AchromaCard>> GetAllCards()
    {
        _logger.LogInformation("Scraping card data...");

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
                facetFilters = new string[] {},
                perPage = 1000,
                page = 1
            }
        };

        try
        {
            var response = await _client.SendQueryAsync<CardResponse>(cardRequest);

            var cards = response.Data.SearchQuery.Items;

            _logger.LogInformation($"Scraped {cards.Count()} cards");

            return cards;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error scraping cards");
            return null;
        }
    }

}
