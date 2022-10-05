using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.Extensions.DependencyInjection;

namespace AchromaBot.Scraper;

public static class DependencyInjection
{
    public static IServiceCollection AddScraperService(this IServiceCollection services) => services
            .AddTransient<GraphQLHttpClient>(_ => new GraphQLHttpClient("https://gateway.achroma.cards/api", new NewtonsoftJsonSerializer()))
            .AddTransient<ScraperService>();
}