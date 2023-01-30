#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Sources/Services/Identity/IdentityServer.API/IdentityServer.API.csproj", "Sources/Services/Identity/IdentityServer.API/"]
COPY ["Sources/BuildingBlocks/Commons/PathProjectChallenge.Logging/PathProjectChallenge.Logging.csproj", "Sources/BuildingBlocks/Commons/PathProjectChallenge.Logging/"]
RUN dotnet restore "Sources/Services/Identity/IdentityServer.API/IdentityServer.API.csproj"
COPY . .
WORKDIR "/src/Sources/Services/Identity/IdentityServer.API"
RUN dotnet build "IdentityServer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityServer.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServer.API.dll"]