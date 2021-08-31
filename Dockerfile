FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

#  Allow swagger.
#ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/NLA.NotificationAPI.RestAPI/NLA.NotificationAPI.RestAPI.csproj", "src/NLA.NotificationAPI.RestAPI/"]
COPY ["src/NLA.NotificationAPI.Contracts/NLA.NotificationAPI.Contracts.csproj", "src/NLA.NotificationAPI.Contracts/"]
COPY ["src/NLA.NotificationAPI.Services/NLA.NotificationAPI.Services.csproj", "src/NLA.NotificationAPI.Services/"]
RUN dotnet restore "src/NLA.NotificationAPI.RestAPI/NLA.NotificationAPI.RestAPI.csproj"
COPY . .
WORKDIR "/src/src/NLA.NotificationAPI.RestAPI"
RUN dotnet build "NLA.NotificationAPI.RestAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NLA.NotificationAPI.RestAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NLA.NotificationAPI.RestAPI.dll"]