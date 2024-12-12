# Gebruik de .NET base image voor ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Gebruik de .NET SDK image voor build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopieer alleen de oplossing en project bestanden
COPY ./TicketBriteAPI/TicketBriteAPI.sln ./TicketBriteAPI/TicketBriteAPI.sln
COPY ./TicketBriteAPI/src/ ./TicketBriteAPI/src/

# Voer dotnet restore uit voor de oplossing
RUN dotnet restore ./TicketBriteAPI/TicketBriteAPI.sln

# Build het project
RUN dotnet build ./TicketBriteAPI/TicketBriteAPI.sln -c Release -o /app/build

# Publiceer het project
FROM build AS publish
RUN dotnet publish ./TicketBriteAPI/TicketBriteAPI.sln -c Release -o /app/publish

# Gebruik de base image en kopieer de gecompileerde bestanden
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "TicketBriteAPI.dll"]