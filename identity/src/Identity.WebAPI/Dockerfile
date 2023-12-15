FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["identity/src/Identity.WebAPI/Identity.WebAPI.csproj", "identity/src/Identity.WebAPI/"]
RUN dotnet restore "identity/src/Identity.WebAPI/Identity.WebAPI.csproj"
COPY . .
WORKDIR "/src/identity/src/Identity.WebAPI"
RUN dotnet build "Identity.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.WebAPI.dll"]
