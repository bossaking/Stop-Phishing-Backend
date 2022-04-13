FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Stop-Phishing.csproj", "./"]
RUN dotnet restore "Stop-Phishing.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "Stop-Phishing.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Stop-Phishing.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY Resources /app/Resources
ENTRYPOINT ["dotnet", "Stop-Phishing.dll"]
