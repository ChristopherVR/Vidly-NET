# Base stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
EXPOSE 80

# Build stage WebAPI
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-web
WORKDIR /src

# Restore nuget packages
COPY ["MovieShowcaseSPA/MovieShowcaseSPA.csproj", "/src/MovieShowcaseSPA/"]
RUN --mount=type=cache,target=/root/.nuget/packages,id=movieshowcase dotnet restore "/src/MovieShowcaseSPA/MovieShowcaseSPA.csproj"

# Copy source files
COPY ["MovieShowcaseSPA", "/src/MovieShowcaseSPA"]

WORKDIR /src/MovieShowcaseSPA

# Build stage (SPA)
FROM node:lts-alpine AS build-spa
WORKDIR /src

# Restore npm packages for the SPA
COPY ["MovieShowcaseSPA/ClientApp/package.json", "/src"]
COPY ["MovieShowcaseSPA/ClientApp/package-lock.json", "/src"]
RUN npm ci

# Copy the source files
COPY ["MovieShowcaseSPA/ClientApp", "/src"]

# Build the SPA
RUN npm run build

# Publish stage
FROM build-web AS publish

RUN --mount=type=cache,target=/root/.nuget/packages,id=movieshowcase dotnet publish "MovieShowcaseSPA.csproj" -c Release -o /app/publish --no-restore

COPY --from=build-spa /src/build /app/publish/ClientApp/build

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MovieShowcaseSPA.dll"]