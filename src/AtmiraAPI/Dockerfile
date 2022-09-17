#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/AtmiraAPI/AtmiraAPI.csproj", "src/AtmiraAPI/"]
COPY ["src/AtmiraAPI.Infrastructure/AtmiraAPI.Infrastructure.csproj", "src/AtmiraAPI.Infrastructure/"]
COPY ["src/AtmiraAPI.Core/AtmiraAPI.Core.csproj", "src/AtmiraAPI.Core/"]
COPY ["src/AtmiraAPI.SharedKernel/AtmiraAPI.SharedKernel.csproj", "src/AtmiraAPI.SharedKernel/"]
RUN dotnet restore "src/AtmiraAPI/AtmiraAPI.csproj"
COPY . .
WORKDIR "/src/src/AtmiraAPI"
RUN dotnet build "AtmiraAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AtmiraAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["src/AtmiraAPI/aspnetapp.pfx", "/https/"]
ENTRYPOINT ["dotnet", "AtmiraAPI.dll"]