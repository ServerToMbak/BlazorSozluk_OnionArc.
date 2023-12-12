#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Projections/BlazorSozluk.Projections.UserService/BlazorSozluk.Projections.UserService.csproj", "src/Projections/BlazorSozluk.Projections.UserService/"]
COPY ["src/Common/BlazorSozuk.Common/BlazorSozluk.Common.csproj", "src/Common/BlazorSozuk.Common/"]
RUN dotnet restore "./src/Projections/BlazorSozluk.Projections.UserService/./BlazorSozluk.Projections.UserService.csproj"
COPY . .
WORKDIR "/src/src/Projections/BlazorSozluk.Projections.UserService"
RUN dotnet build "./BlazorSozluk.Projections.UserService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BlazorSozluk.Projections.UserService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorSozluk.Projections.UserService.dll"]