#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Sources/Services/Catalog/Catalog.API/Catalog.API.csproj", "Sources/Services/Catalog/Catalog.API/"]
COPY ["Sources/Services/Catalog/Catalog.Service/Catalog.Service.csproj", "Sources/Services/Catalog/Catalog.Service/"]
COPY ["Sources/BuildingBlocks/Commons/PathProjectChallenge.Core/PathProjectChallenge.Core.csproj", "Sources/BuildingBlocks/Commons/PathProjectChallenge.Core/"]
COPY ["Sources/BuildingBlocks/Commons/PathProjectChallenge.Data/PathProjectChallenge.Data.csproj", "Sources/BuildingBlocks/Commons/PathProjectChallenge.Data/"]
COPY ["Sources/BuildingBlocks/Commons/PathProjectChallenge.Service/PathProjectChallenge.Service.csproj", "Sources/BuildingBlocks/Commons/PathProjectChallenge.Service/"]
COPY ["Sources/Services/Catalog/Catalog.Core/Catalog.Core.csproj", "Sources/Services/Catalog/Catalog.Core/"]
COPY ["Sources/Services/Catalog/Catalog.Data/Catalog.Data.csproj", "Sources/Services/Catalog/Catalog.Data/"]
COPY ["Sources/Services/Catalog/Catalog.Dto/Catalog.Dto.csproj", "Sources/Services/Catalog/Catalog.Dto/"]
RUN dotnet restore "Sources/Services/Catalog/Catalog.API/Catalog.API.csproj"
COPY . .
WORKDIR "/src/Sources/Services/Catalog/Catalog.API"
RUN dotnet build "Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]