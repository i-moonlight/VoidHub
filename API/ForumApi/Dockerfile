# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /App

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App

COPY --from=build /App/out .

EXPOSE 5000

CMD ["dotnet", "ForumApi.dll"]

# migration only by hands (