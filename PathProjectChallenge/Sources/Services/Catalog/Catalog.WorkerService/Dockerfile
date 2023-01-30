#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Sources/Services/Catalog/Catalog.WorkerService/Catalog.WorkerService.csproj", "Sources/Services/Catalog/Catalog.WorkerService/"]
COPY ["Sources/Services/Catalog/Catalog.Core/Catalog.Core.csproj", "Sources/Services/Catalog/Catalog.Core/"]
COPY ["Sources/BuildingBlocks/Commons/PathProjectChallenge.Core/PathProjectChallenge.Core.csproj", "Sources/BuildingBlocks/Commons/PathProjectChallenge.Core/"]
COPY ["Sources/Services/Catalog/Catalog.Dto/Catalog.Dto.csproj", "Sources/Services/Catalog/Catalog.Dto/"]
COPY ["Sources/Services/Catalog/Catalog.Service/Catalog.Service.csproj", "Sources/Services/Catalog/Catalog.Service/"]
COPY ["Sources/BuildingBlocks/Commons/PathProjectChallenge.Data/PathProjectChallenge.Data.csproj", "Sources/BuildingBlocks/Commons/PathProjectChallenge.Data/"]
COPY ["Sources/BuildingBlocks/Commons/PathProjectChallenge.Service/PathProjectChallenge.Service.csproj", "Sources/BuildingBlocks/Commons/PathProjectChallenge.Service/"]
COPY ["Sources/Services/Catalog/Catalog.Data/Catalog.Data.csproj", "Sources/Services/Catalog/Catalog.Data/"]
RUN dotnet restore "Sources/Services/Catalog/Catalog.WorkerService/Catalog.WorkerService.csproj"
COPY . .
WORKDIR "/src/Sources/Services/Catalog/Catalog.WorkerService"
RUN dotnet build "Catalog.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.WorkerService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.WorkerService.dll"]