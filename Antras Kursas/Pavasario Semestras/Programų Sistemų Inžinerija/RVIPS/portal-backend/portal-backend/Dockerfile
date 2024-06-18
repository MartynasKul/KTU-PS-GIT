FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["portal-backend.csproj", "./"]
RUN dotnet restore "portal-backend.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "portal-backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "portal-backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "portal-backend.dll"]
