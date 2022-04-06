FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Stop-Phishing/Stop-Phishing.csproj", "Stop-Phishing/"]
RUN dotnet restore "Stop-Phishing/Stop-Phishing.csproj"
COPY . .
WORKDIR "/src/Stop-Phishing"
RUN dotnet build "Stop-Phishing.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Stop-Phishing.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stop-Phishing.dll"]
