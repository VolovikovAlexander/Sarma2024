# Образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /build

# Копирование файлов
COPY . .

# Что получилось?
WORKDIR /build
RUN echo $(ls -a)

# Запускаем сборку
RUN dotnet restore GeoJsonConvertor.sln
RUN dotnet build GeoJsonConvertor.sln  -c Debug -o /app/publish

# Что получилось?
WORKDIR /app/publish
RUN echo $(ls -a)

# Образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:7.0  AS base
WORKDIR /app

# Загружаем результаты в образ для запуска
COPY --from=build /app/publish .

# Указываем точку запуска
ENTRYPOINT ["dotnet", "GeoJsonConvertor.dll"]


