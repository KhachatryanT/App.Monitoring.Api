FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App.Monitoring.Host/App.Monitoring.Host.csproj", "App.Monitoring.Host/"]
COPY ["App.Monitoring.Api/App.Monitoring.Api.csproj", "App.Monitoring.Api/"]
COPY ["App.Monitoring.UseCases/App.Monitoring.UseCases.csproj", "App.Monitoring.UseCases/"]
COPY ["App.Monitoring.Infrastructure.Interfaces/App.Monitoring.Infrastructure.Interfaces.csproj", "App.Monitoring.Infrastructure.Interfaces/"]
COPY ["App.Monitoring.Entities/App.Monitoring.Entities.csproj", "App.Monitoring.Entities/"]
COPY ["App.Monitoring.DataAccess.Dapper.Postgresql/App.Monitoring.DataAccess.Dapper.Postgresql.csproj", "App.Monitoring.DataAccess.Dapper.Postgresql/"]
COPY ["App.Monitoring.Infrastructure.Implementation/App.Monitoring.Infrastructure.Implementation.csproj", "App.Monitoring.Infrastructure.Implementation/"]
RUN dotnet restore "App.Monitoring.Host/App.Monitoring.Host.csproj"
COPY . .
WORKDIR "/src/App.Monitoring.Host"
RUN dotnet build "App.Monitoring.Host.csproj" -c Release

FROM build AS publish
RUN dotnet publish "App.Monitoring.Host.csproj" --no-build -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "App.Monitoring.Host.dll"]
