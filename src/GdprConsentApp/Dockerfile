FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/GdprConsentApp/GdprConsentApp.csproj", "src/GdprConsentApp/"]
RUN dotnet restore "src/GdprConsentApp/GdprConsentApp.csproj"
COPY . .
WORKDIR "/src/src/GdprConsentApp"
RUN dotnet build "GdprConsentApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GdprConsentApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GdprConsentApp.dll"]
