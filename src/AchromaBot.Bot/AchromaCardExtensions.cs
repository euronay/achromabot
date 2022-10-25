namespace AchromaBot.Bot;

public static class StringExtensions
{
    public static string DashIfEmpty(this string value) => string.IsNullOrWhiteSpace(value) ? "-" : value;
}