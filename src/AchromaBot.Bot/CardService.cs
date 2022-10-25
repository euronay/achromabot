using AchromaBot.Common;
using Newtonsoft.Json;
using FuzzySharp;
using FuzzySharp.SimilarityRatio;
using FuzzySharp.SimilarityRatio.Scorer.Composite;
using Microsoft.Extensions.Logging;

namespace AchromaBot.Bot;

public class CardService
{
    private IEnumerable<AchromaCard> _cards;
    private readonly ILogger _logger;

    public CardService(ILogger<CardService> logger) =>_logger = logger;
    

    public void InitCards(IEnumerable<AchromaCard> cards)
    {
        _logger.LogInformation($"Initializing CardService with {cards.Count()} cards");
        _cards = cards;
    }
    

    public AchromaCard GetSingleCard(string name)
    {
        var match = Process.ExtractOne(name.ToLower(), _cards.Select(c => c.Name.ToLower()), s => s, ScorerCache.Get<WeightedRatioScorer>());

        _logger.LogInformation($"Searching for '{name}'. Found card '{match.Value}', score: {match.Score}.");

        return match.Score > 80 ? _cards.FirstOrDefault(c => c.Name.ToLower() == match.Value) : null;
    }
}