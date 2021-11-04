FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/MusicCatalog.IdentityApi/MusicCatalog.IdentityApi.csproj", "src/MusicCatalog.IdentityApi/"]
RUN dotnet restore "src/MusicCatalog.IdentityApi/MusicCatalog.IdentityApi.csproj"
COPY . .
WORKDIR "/src/src/MusicCatalog.IdentityApi"
RUN dotnet build "MusicCatalog.IdentityApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusicCatalog.IdentityApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusicCatalog.IdentityApi.dll"]