FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR ./
COPY ./TicketBriteAPI/TicketBriteAPI.sln ./TicketBriteAPI/TicketBriteAPI.sln
COPY ./TicketBriteAPI/Controllers/ ./TicketBriteAPI/Controllers/
COPY ./TicketBriteAPI/Models/ ./TicketBriteAPI/Models/
COPY ./TicketBriteAPI/Services/ ./TicketBriteAPI/Services/
COPY ./TicketBriteAPI/Program.cs ./TicketBriteAPI/Program.cs
COPY ./TicketBriteAPI/appsettings.json ./TicketBriteAPI/appsettings.json
COPY ./TicketBriteAPI/appsettings.Development.json ./TicketBriteAPI/appsettings.Development.json

# Voer dotnet restore uit voor de oplossing
RUN dotnet restore ./TicketBriteAPI/TicketBriteAPI.sln

# Bouw de applicatie
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "TicketBriteAPI.dll"]
