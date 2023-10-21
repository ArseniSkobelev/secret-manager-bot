FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SecretManagerBot/SecretManagerBot.csproj", "SecretManagerBot/"]
RUN dotnet restore "SecretManagerBot/SecretManagerBot.csproj"
COPY . .
WORKDIR "/src/SecretManagerBot"
RUN dotnet build "SecretManagerBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SecretManagerBot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecretManagerBot.dll"]
