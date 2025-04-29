FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["SportsHall/SportsHall.csproj", "SportsHall/"]
RUN dotnet restore "./SportsHall/SportsHall.csproj"
COPY . .
WORKDIR "/src/SportsHall"
RUN dotnet build "./SportsHall.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./SportsHall.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SportsHall.dll"]