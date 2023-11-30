# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

COPY --from=build /App/out .
COPY ./appsettings.Docker.json ./appsettings.json

EXPOSE 5000

CMD ["dotnet", "ForumApi.dll"]

# migration only by hands (