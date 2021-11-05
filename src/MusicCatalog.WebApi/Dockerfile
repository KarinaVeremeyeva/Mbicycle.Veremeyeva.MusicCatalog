FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80  

FROM mcr.microsoft.com/dotnet/sdk:5 AS build
WORKDIR /src
COPY ["src/MusicCatalog.WebApi/MusicCatalog.WebApi.csproj", "src/MusicCatalog.WebApi/"]
COPY ["src/MusicCatalog.BusinessLogic/MusicCatalog.BusinessLogic.csproj", "src/MusicCatalog.BusinessLogic/"]
COPY ["src/MusicCatalog.DataAccess/MusicCatalog.DataAccess.csproj", "src/MusicCatalog.DataAccess/"]
RUN dotnet restore "src/MusicCatalog.WebApi/MusicCatalog.WebApi.csproj"
COPY . .
WORKDIR "/src/src/MusicCatalog.WebApi"
RUN dotnet build "MusicCatalog.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusicCatalog.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusicCatalog.WebApi.dll"]