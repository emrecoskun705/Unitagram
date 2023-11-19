#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/API/Unitagram.WebAPI/Unitagram.WebAPI.csproj", "src/API/Unitagram.WebAPI/"]
COPY ["src/Core/Unitagram.Application/Unitagram.Application.csproj", "src/Core/Unitagram.Application/"]
COPY ["src/Core/Unitagram.Domain/Unitagram.Domain.csproj", "src/Core/Unitagram.Domain/"]
COPY ["src/Infrastructure/Unitagram.Infrastructure/Unitagram.Infrastructure.csproj", "src/Infrastructure/Unitagram.Infrastructure/"]
COPY ["src/Infrastructure/Unitagram.Persistence/Unitagram.Persistence.csproj", "src/Infrastructure/Unitagram.Persistence/"]
RUN dotnet restore "src/API/Unitagram.WebAPI/Unitagram.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/API/Unitagram.WebAPI"
RUN dotnet build "Unitagram.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Unitagram.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Unitagram.WebAPI.dll"]