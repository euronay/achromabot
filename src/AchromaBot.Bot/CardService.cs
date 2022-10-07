using AchromaBot.Common;
using Newtonsoft.Json;
using System.Linq;
using System.Reflection;

namespace AchromaBot.Bot;

public class CardService
{
    private List<AchromaCard> _cards;
    private readonly AppSettings _appSettings;

    public CardService(AppSettings appSettings)
    { 
        _appSettings = appSettings;
        _cards = new List<AchromaCard>();

        foreach(var file in Directory.GetFiles(_appSettings.DataPath, "*.json"))
        {
            Console.WriteLine($"Reading {file}...");

            var cardSetData = File.ReadAllText(file);
            var cardSet = JsonConvert.DeserializeObject<AchromaCard[]>(cardSetData);
            
            _cards.AddRange(cardSet);
        }
    }

    public async Task<AchromaCard> GetSingleCardAsync(string name)
    {
        return _cards.FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
    }
}