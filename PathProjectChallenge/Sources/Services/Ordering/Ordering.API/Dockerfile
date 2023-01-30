#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Sources/Services/Ordering/Ordering.API/Ordering.API.csproj", "Sources/Services/Ordering/Ordering.API/"]
COPY ["Sources/Services/Ordering/Ordering.Core/Ordering.Core.csproj", "Sources/Services/Ordering/Ordering.Core/"]
COPY ["Sources/Services/Ordering/Ordering.Dto/Ordering.Dto.csproj", "Sources/Services/Ordering/Ordering.Dto/"]
RUN dotnet restore "Sources/Services/Ordering/Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/Sources/Services/Ordering/Ordering.API"
RUN dotnet build "Ordering.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]