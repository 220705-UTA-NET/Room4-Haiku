# syntax=docker/dockerfile:1 runtime:6.0 # to user runtime instead
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env 
WORKDIR /MyApi.App

# Copy csproj and restore as distinct layers
#COPY ./MyApi.App/*.csproj ./
COPY . .
RUN dotnet restore

# Copy everything else and build
# COPY ../engine/examples ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /MyApi.App/out .
ENTRYPOINT ["dotnet", "MyApi.App.dll"]

