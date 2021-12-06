FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Mk.AuthServer.API/Mk.AuthServer.API.csproj", "Mk.AuthServer.API/"]
RUN dotnet restore "Mk.AuthServer.API/Mk.AuthServer.API.csproj"
COPY . .
WORKDIR "/src/Mk.AuthServer.API"
RUN dotnet build "Mk.AuthServer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mk.AuthServer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mk.AuthServer.API.dll"]
