FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
    
COPY ./src/ ./
RUN dotnet publish ./AchromaBot.Bot/AchromaBot.Bot.csproj -c Release -o out


FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "AchromaBot.Bot.dll"]

