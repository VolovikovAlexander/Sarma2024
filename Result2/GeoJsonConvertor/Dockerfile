
# Образ для сборки
# https://learn.microsoft.com/en-us/dotnet/core/docker/build-container?tabs=linux&pivots=dotnet-8-0
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Копирование файлов
COPY . ./

# Запускаем сборку
RUN dotnet restore "GeoJsonConvertor.sln"
RUN dotnet publish "GeoJsonConvertor.sln"  -c Debug -o out

# Запуск
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "GeoJsonConvertor.dll"]
CMD ["template.geojson"]


