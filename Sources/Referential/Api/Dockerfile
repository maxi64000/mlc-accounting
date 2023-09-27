#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://*:5080;http://*:5081
ENV ASPNETCORE_HTTP_PORT=5080
ENV MANAGEMENT_HTTP_PORT=5081
EXPOSE 5080 5081

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sources/Referential/Api/MlcAccounting.Referential.Api.csproj", "Sources/Referential/Api/"]
COPY ["Sources/Referential/Infrastructure/MlcAccounting.Referential.Infrastructure.csproj", "Sources/Referential/Infrastructure/"]
COPY ["Sources/Referential/Domain/MlcAccounting.Referential.Domain.csproj", "Sources/Referential/Domain/"]
COPY ["Sources/Common/MlcAccounting.Common.csproj", "Sources/Common/"]
RUN dotnet restore "Sources/Referential/Api/MlcAccounting.Referential.Api.csproj"
COPY . .
WORKDIR "/src/Sources/Referential/Api"
RUN dotnet build "MlcAccounting.Referential.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MlcAccounting.Referential.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MlcAccounting.Referential.Api.dll"]