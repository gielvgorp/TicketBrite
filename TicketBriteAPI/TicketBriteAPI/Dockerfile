# Stap 1: Gebaseerd op een officiële .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Zet de werkmap
WORKDIR /src

# Kopieer de .csproj-bestanden en herstel de afhankelijkheden
COPY ["TicketBriteAPI/TicketBriteAPI.csproj", "TicketBriteAPI/"]
RUN dotnet restore "TicketBriteAPI/TicketBriteAPI.csproj"

# Kopieer de rest van de code
COPY . .

# Stap 2: Bouw de API
WORKDIR "/src/TicketBriteAPI"
RUN dotnet build "TicketBriteAPI.csproj" -c Release -o /app/build

# Stap 3: Publiceer de API
RUN dotnet publish "TicketBriteAPI.csproj" -c Release -o /app/publish

# Stap 4: Maak de runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Stel de poort in
EXPOSE 7150

# Start de API
ENTRYPOINT ["dotnet", "TicketBriteAPI.dll"]
