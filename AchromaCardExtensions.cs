public static class AchromaCardExtensions
{
    public static AchromaCard GetClosestMatch(this IEnumerable<AchromaCard> cards, string name) => cards.Count() switch
    {
        0 => null,
        1 => cards.First(),
        _ => cards.FirstOrDefault(c => c.Name.ToLower() == name)
    };
}