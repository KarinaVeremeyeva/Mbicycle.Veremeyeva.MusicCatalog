FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app
EXPOSE 80

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "MusicCatalog.IdentityApi.dll"]