#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BargainIt.Api/BargainIt.Api.csproj", "BargainIt.Api/"]
COPY ["BargainIt.Application/BargainIt.Application.csproj", "BargainIt.Application/"]
COPY ["BargainIt.Persistence/BargainIt.Persistence.csproj", "BargainIt.Persistence/"]
COPY ["BargainIt.Shared/BargainIt.Shared.csproj", "BargainIt.Shared/"]
COPY ["BargainIt.Infrastructure/BargainIt.Infrastructure.csproj", "BargainIt.Infrastructure/"]
RUN dotnet restore "BargainIt.Api/BargainIt.Api.csproj"
COPY . .
WORKDIR "/src/BargainIt.Api"
RUN dotnet build "BargainIt.Api.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "BargainIt.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ARG ENVIRONMENT
ENV ASPNETCORE_ENVIRONMENT ${ENVIRONMENT}
ENTRYPOINT ["dotnet", "BargainIt.Api.dll"]