#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://*:5082;http://*:5083
ENV ASPNETCORE_HTTP_PORT=5082
ENV MANAGEMENT_HTTP_PORT=5083
EXPOSE 5082 5083

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sources/Integration/Api/MlcAccounting.Integration.Api.csproj", "Sources/Integration/Api/"]
COPY ["Sources/Integration/Domain/MlcAccounting.Integration.Domain.csproj", "Sources/Integration/Domain/"]
COPY ["Sources/Common/MlcAccounting.Common.csproj", "Sources/Common/"]
COPY ["Sources/Integration/Infrastructure/MlcAccounting.Integration.Infrastructure.csproj", "Sources/Integration/Infrastructure/"]
RUN dotnet restore "Sources/Integration/Api/MlcAccounting.Integration.Api.csproj"
COPY . .
WORKDIR "/src/Sources/Integration/Api"
RUN dotnet build "MlcAccounting.Integration.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MlcAccounting.Integration.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MlcAccounting.Integration.Api.dll"]