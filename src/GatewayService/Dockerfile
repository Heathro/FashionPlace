FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80

COPY FashionPlace.sln FashionPlace.sln
COPY src/CatalogService/CatalogService.csproj src/CatalogService/CatalogService.csproj
COPY src/GatewayService/GatewayService.csproj src/GatewayService/GatewayService.csproj
COPY src/IdentityService/IdentityService.csproj src/IdentityService/IdentityService.csproj
COPY src/NotificationService/NotificationService.csproj src/NotificationService/NotificationService.csproj
COPY src/SearchService/SearchService.csproj src/SearchService/SearchService.csproj

RUN dotnet restore FashionPlace.sln

COPY src/GatewayService src/GatewayService
WORKDIR /app/src/GatewayService
RUN dotnet publish -c Release -o /app/src/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet", "GatewayService.dll" ]