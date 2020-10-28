#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["StarWars.API/StarWars.API.csproj", "StarWars.API/"]
COPY ["StarWars.DataAccess/StarWars.DataAccess.csproj", "StarWars.DataAccess/"]
COPY ["StarWars.Core/StarWars.Core.csproj", "StarWars.Core/"]
RUN dotnet restore "StarWars.API/StarWars.API.csproj"
COPY . .
WORKDIR "/src/StarWars.API"
RUN dotnet build "StarWars.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StarWars.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StarWars.API.dll"]