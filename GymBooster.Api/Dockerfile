#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GymBooster.Api/GymBooster.Api.csproj", "GymBooster.Api/"]
COPY ["GymBooster.Common.Utils/GymBooster.Common.Utils.csproj", "GymBooster.Common.Utils/"]
COPY ["GymBooster.Common.Objects/GymBooster.Common.Objects.csproj", "GymBooster.Common.Objects/"]
RUN dotnet restore "GymBooster.Api/GymBooster.Api.csproj"
COPY . .
WORKDIR "/src/GymBooster.Api"
RUN dotnet build "GymBooster.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GymBooster.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GymBooster.Api.dll"]