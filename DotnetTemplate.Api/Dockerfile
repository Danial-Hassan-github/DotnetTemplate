FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DotnetTemplate.Api/DotnetTemplate.Api.csproj", "DotnetTemplate.Api/"]
COPY ["DotnetTemplate.Application/DotnetTemplate.Application.csproj", "DotnetTemplate.Application/"]
COPY ["DotnetTemplate.Domain/DotnetTemplate.Domain.csproj", "DotnetTemplate.Domain/"]
COPY ["DotnetTemplate.Infrastructure/DotnetTemplate.Infrastructure.csproj", "DotnetTemplate.Infrastructure/"]
RUN dotnet restore "./DotnetTemplate.Api/DotnetTemplate.Api.csproj"
COPY . .
WORKDIR "/src/DotnetTemplate.Api"
RUN dotnet build "./DotnetTemplate.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DotnetTemplate.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotnetTemplate.Api.dll"]