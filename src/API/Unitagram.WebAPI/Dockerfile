FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/API/Unitagram.WebAPI/Unitagram.WebAPI.csproj", "src/API/Unitagram.WebAPI/"]
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
