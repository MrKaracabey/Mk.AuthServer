FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Miniapp3.API/Miniapp3.API.csproj", "Miniapp3.API/"]
RUN dotnet restore "Miniapp3.API/Miniapp3.API.csproj"
COPY . .
WORKDIR "/src/Miniapp3.API"
RUN dotnet build "Miniapp3.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Miniapp3.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Miniapp3.API.dll"]
