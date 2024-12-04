# Gebruik een officiële .NET runtime image als basis
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Gebruik een officiële .NET SDK image voor de build fase
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Kopieer alleen het projectbestand en herstel dependencies
COPY ["TicketBriteAPI/TicketBriteAPI.csproj", "TicketBriteAPI/"]
RUN dotnet restore "TicketBriteAPI/TicketBriteAPI.csproj"

# Kopieer de rest van de broncode en bouw de applicatie
COPY . .
WORKDIR "/src/TicketBriteAPI"
RUN dotnet build "TicketBriteAPI.csproj" -c Release -o /app/build

# Publiceer de applicatie
FROM build AS publish
RUN dotnet publish "TicketBriteAPI.csproj" -c Release -o /app/publish

# Gebruik de runtime image om de applicatie uit te voeren
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketBriteAPI.dll"]
