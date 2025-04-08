FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development
ENV LANG=C.UTF-8
ENV LC_ALL=C.UTF-8

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /ExcursionAPI

COPY ["ExcursionAPI/ExcursionAPI.csproj", "ExcursionAPI/"]
COPY ["BusinessLogic/BusinessLogic.csproj", "BusinessLogic/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
RUN dotnet restore "ExcursionAPI/ExcursionAPI.csproj"

COPY . .
FROM build as publish
RUN dotnet publish "ExcursionAPI/ExcursionAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base as final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExcursionAPI.dll"]