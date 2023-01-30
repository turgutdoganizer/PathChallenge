#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Sources/Services/Ordering/Ordering.Event.WorkerService/Ordering.Event.WorkerService.csproj", "Sources/Services/Ordering/Ordering.Event.WorkerService/"]
COPY ["Sources/BuildingBlocks/Commons/PathProjectChallenge.Logging/PathProjectChallenge.Logging.csproj", "Sources/BuildingBlocks/Commons/PathProjectChallenge.Logging/"]
COPY ["Sources/Services/Ordering/Ordering.Core/Ordering.Core.csproj", "Sources/Services/Ordering/Ordering.Core/"]
COPY ["Sources/Services/Ordering/Ordering.Data/Ordering.Data.csproj", "Sources/Services/Ordering/Ordering.Data/"]
RUN dotnet restore "Sources/Services/Ordering/Ordering.Event.WorkerService/Ordering.Event.WorkerService.csproj"
COPY . .
WORKDIR "/src/Sources/Services/Ordering/Ordering.Event.WorkerService"
RUN dotnet build "Ordering.Event.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.Event.WorkerService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.Event.WorkerService.dll"]