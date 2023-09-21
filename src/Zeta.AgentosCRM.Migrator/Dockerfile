#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Zeta.AgentosCRM.Migrator/Zeta.AgentosCRM.Migrator.csproj", "src/Zeta.AgentosCRM.Migrator/"]
COPY ["src/Zeta.AgentosCRM.EntityFrameworkCore/Zeta.AgentosCRM.EntityFrameworkCore.csproj", "src/Zeta.AgentosCRM.EntityFrameworkCore/"]
COPY ["src/Zeta.AgentosCRM.Core/Zeta.AgentosCRM.Core.csproj", "src/Zeta.AgentosCRM.Core/"]
COPY ["src/Zeta.AgentosCRM.Core.Shared/Zeta.AgentosCRM.Core.Shared.csproj", "src/Zeta.AgentosCRM.Core.Shared/"]
RUN dotnet restore "src/Zeta.AgentosCRM.Migrator/Zeta.AgentosCRM.Migrator.csproj"
COPY . .
WORKDIR "/src/src/Zeta.AgentosCRM.Migrator"
RUN dotnet build "Zeta.AgentosCRM.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zeta.AgentosCRM.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zeta.AgentosCRM.Migrator.dll", "-s"]
