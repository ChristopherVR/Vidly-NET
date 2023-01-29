#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
# Restore nuget packages
COPY ["Services/MovieSystem/MovieSystem.API/MovieSystem.API.csproj", "MovieSystem/MovieSystem.API/"]
COPY ["Services/MovieSystem/MovieSystem.Domain/MovieSystem.Domain.csproj", "MovieSystem/MovieSystem.Domain/"]
COPY ["Services/MovieSystem/MovieSystem.Infrastructure/MovieSystem.Infrastructure.csproj", "MovieSystem/MovieSystem.Infrastructure/"]

RUN dotnet restore "MovieSystem/MovieSystem.API/MovieSystem.API.csproj"

# Copy source files
COPY [".editorconfig", "."]
COPY ["Services/MovieSystem/MovieSystem.API", "MovieSystem/MovieSystem.API"]
COPY ["Services/MovieSystem/MovieSystem.Domain", "MovieSystem/MovieSystem.Domain"]
COPY ["Services/MovieSystem/MovieSystem.Infrastructure", "MovieSystem/MovieSystem.Infrastructure"]

COPY . .
WORKDIR "/src/Services/MovieSystem/MovieSystem.API"
RUN dotnet build "MovieSystem.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieSystem.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieSystem.API.dll"]
