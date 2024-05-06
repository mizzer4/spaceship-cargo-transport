#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SpaceshipCargoTransport.Application/SpaceshipCargoTransport.Application.csproj", "SpaceshipCargoTransport.Application/"]
COPY ["SpaceshipCargoTransport.Persistence/SpaceshipCargoTransport.Persistence.csproj", "SpaceshipCargoTransport.Persistence/"]
COPY ["SpaceshipCargoTransport.Domain/SpaceshipCargoTransport.Domain.csproj", "SpaceshipCargoTransport.Domain/"]
RUN dotnet restore "SpaceshipCargoTransport.Application/SpaceshipCargoTransport.Application.csproj"
COPY . .
WORKDIR "/src/SpaceshipCargoTransport.Application"
RUN dotnet build "SpaceshipCargoTransport.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpaceshipCargoTransport.Application.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpaceshipCargoTransport.Application.dll"]