FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Lab7_IntegralByCircle/Lab7_IntegralByCircle.csproj", "Lab7_IntegralByCircle/"]
RUN dotnet restore "Lab7_IntegralByCircle/Lab7_IntegralByCircle.csproj"
COPY . .
WORKDIR "/src/Lab7_IntegralByCircle"
RUN dotnet build "Lab7_IntegralByCircle.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lab7_IntegralByCircle.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab7_IntegralByCircle.dll"]
