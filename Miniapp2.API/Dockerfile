FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Miniapp2.API/Miniapp2.API.csproj", "Miniapp2.API/"]
RUN dotnet restore "Miniapp2.API/Miniapp2.API.csproj"
COPY . .
WORKDIR "/src/Miniapp2.API"
RUN dotnet build "Miniapp2.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Miniapp2.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Miniapp2.API.dll"]
