FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7150

COPY wait-for-it.sh /wait-for-it.sh
RUN chmod +x /wait-for-it.sh

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR ./
COPY TicketBriteAPI.sln ./
COPY TicketBriteAPI/Controllers/ ./TicketBriteAPI/Controllers/
COPY TicketBriteAPI/Models/ ./TicketBriteAPI/Models/
COPY TicketBriteAPI/Services/ ./TicketBriteAPI/Services/
COPY TicketBriteAPI/Program.cs ./TicketBriteAPI/Program.cs
COPY TicketBriteAPI/TicketBriteAPI.csproj ./TicketBriteAPI/TicketBriteAPI.csproj
COPY TicketBriteAPI/appsettings.json ./TicketBriteAPI/appsettings.json
COPY TicketBriteAPI/appsettings.Development.json ./TicketBriteAPI/appsettings.Development.json

# Kopieer de andere projecten (Core, Data, Test)
COPY TicketBrite.Core/ ./TicketBrite.Core/
COPY TicketBrite.Data/ ./TicketBrite.Data/
COPY TicketBrite.Test/ ./TicketBrite.Test/
COPY TicketBrite.DTO/ ./TicketBrite.DTO/
COPY TicketBriteAPI/ ./TicketBriteAPI/

# Voer dotnet restore uit voor de oplossing
RUN dotnet restore ./TicketBriteAPI.sln

# Bouw de applicatie
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "TicketBriteAPI.dll"]
ENTRYPOINT ["/wait-for-it.sh", "sqlserver-container:1433", "--", "dotnet", "TicketBriteAPI.dll"]