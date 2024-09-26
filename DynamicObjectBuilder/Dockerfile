FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/DynamicObjectBuilder.Api/DynamicObjectBuilder.Api.csproj", "src/DynamicObjectBuilder.Api/"]
COPY ["src/DynamicObjectBuilder.Business/DynamicObjectBuilder.Business.csproj", "src/DynamicObjectBuilder.Business/"]
COPY ["src/DynamicObjectBuilder.Contracts/DynamicObjectBuilder.Contracts.csproj", "src/DynamicObjectBuilder.Contracts/"]
COPY ["src/DynamicObjectBuilder.DataAccess/DynamicObjectBuilder.DataAccess.csproj", "src/DynamicObjectBuilder.DataAccess/"]
RUN dotnet restore "./src/DynamicObjectBuilder.Api/DynamicObjectBuilder.Api.csproj"
COPY . .
WORKDIR "/src/src/DynamicObjectBuilder.Api"
RUN dotnet build "./DynamicObjectBuilder.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DynamicObjectBuilder.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DynamicObjectBuilder.Api.dll"]