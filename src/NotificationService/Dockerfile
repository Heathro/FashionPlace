FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80

COPY FashionPlace.sln FashionPlace.sln
COPY src/AIService/AIService.csproj src/AIService/AIService.csproj
COPY src/CatalogService/CatalogService.csproj src/CatalogService/CatalogService.csproj
COPY src/GatewayService/GatewayService.csproj src/GatewayService/GatewayService.csproj
COPY src/IdentityService/IdentityService.csproj src/IdentityService/IdentityService.csproj
COPY src/NotificationService/NotificationService.csproj src/NotificationService/NotificationService.csproj
COPY src/SearchService/SearchService.csproj src/SearchService/SearchService.csproj
COPY tests/CatalogService.UnitTests/CatalogService.UnitTests.csproj tests/CatalogService.UnitTests/CatalogService.UnitTests.csproj

RUN dotnet restore FashionPlace.sln

COPY src/NotificationService src/NotificationService
WORKDIR /app/src/NotificationService
RUN dotnet publish -c Release -o /app/src/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet", "NotificationService.dll" ]