FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TicketBriteAPI/TicketBriteAPI.csproj", "TicketBriteAPI/"]
RUN dotnet restore "TicketBriteAPI/TicketBriteAPI.csproj"
COPY . .
WORKDIR "/src/TicketBriteAPI"
RUN dotnet build "TicketBriteAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicketBriteAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketBriteAPI.dll"]
