#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/api/WebApi/BlazorSozluk.Api.WebApi/BlazorSozluk.Api.WebApi.csproj", "src/api/WebApi/BlazorSozluk.Api.WebApi/"]
COPY ["src/api/Core/BlazorSozluk.Api.Application/BlazorSozluk.Api.Application.csproj", "src/api/Core/BlazorSozluk.Api.Application/"]
COPY ["src/Common/BlazorSozuk.Common/BlazorSozluk.Common.csproj", "src/Common/BlazorSozuk.Common/"]
COPY ["src/api/Core/BlazorSozluk.Api.Domain/BlazorSozluk.Api.Domain.csproj", "src/api/Core/BlazorSozluk.Api.Domain/"]
COPY ["src/api/Infrastructure/BlazorSozluk.Infrastructure.Persistence/BlazorSozluk.Api.Infrastructure.Persistence.csproj", "src/api/Infrastructure/BlazorSozluk.Infrastructure.Persistence/"]
RUN dotnet restore "./src/api/WebApi/BlazorSozluk.Api.WebApi/./BlazorSozluk.Api.WebApi.csproj"
COPY . .
WORKDIR "/src/src/api/WebApi/BlazorSozluk.Api.WebApi"
RUN dotnet build "./BlazorSozluk.Api.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BlazorSozluk.Api.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorSozluk.Api.WebApi.dll"]