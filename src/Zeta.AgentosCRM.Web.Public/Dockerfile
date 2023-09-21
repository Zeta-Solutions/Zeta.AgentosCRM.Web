#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Zeta.AgentosCRM.Web.Public/Zeta.AgentosCRM.Web.Public.csproj", "src/Zeta.AgentosCRM.Web.Public/"]
COPY ["src/Zeta.AgentosCRM.Web.Core/Zeta.AgentosCRM.Web.Core.csproj", "src/Zeta.AgentosCRM.Web.Core/"]
COPY ["src/Zeta.AgentosCRM.Application/Zeta.AgentosCRM.Application.csproj", "src/Zeta.AgentosCRM.Application/"]
COPY ["src/Zeta.AgentosCRM.Application.Shared/Zeta.AgentosCRM.Application.Shared.csproj", "src/Zeta.AgentosCRM.Application.Shared/"]
COPY ["src/Zeta.AgentosCRM.Core.Shared/Zeta.AgentosCRM.Core.Shared.csproj", "src/Zeta.AgentosCRM.Core.Shared/"]
COPY ["src/Zeta.AgentosCRM.Core/Zeta.AgentosCRM.Core.csproj", "src/Zeta.AgentosCRM.Core/"]
COPY ["src/Zeta.AgentosCRM.EntityFrameworkCore/Zeta.AgentosCRM.EntityFrameworkCore.csproj", "src/Zeta.AgentosCRM.EntityFrameworkCore/"]
COPY ["src/Zeta.AgentosCRM.GraphQL/Zeta.AgentosCRM.GraphQL.csproj", "src/Zeta.AgentosCRM.GraphQL/"]
RUN dotnet restore "src/Zeta.AgentosCRM.Web.Public/Zeta.AgentosCRM.Web.Public.csproj"
COPY . .
WORKDIR "/src/src/Zeta.AgentosCRM.Web.Public"
RUN dotnet build "Zeta.AgentosCRM.Web.Public.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zeta.AgentosCRM.Web.Public.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zeta.AgentosCRM.Web.Public.dll"]
