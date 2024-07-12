# Пример работы с базой данных

1. Исправим gitignore
```
# Custom
pgadmin/
pgdata/
```

2. Запускаем контейнеры
```bash
sudo docker-compose up -d
```

3. Создаем базу данных
```sql
create unique index ix_unique_name on public.reqions(name);

ALTER TABLE public.fire_history
ADD CONSTRAINT fire_history_region_id FOREIGN KEY (region_id) REFERENCES public.reqions (id);
```

4. Создаем новое консольное приложение
```bash
dotnet new console --name GeoJsonStorage
```
Переносим часть файлов из прошлого проекта.


5. Добавим файл настроек `appsettings.json`
https://ballardsoftware.com/adding-appsettings-json-configuration-to-a-net-core-console-application/
https://connectionstrings.com/

Установим Nuget пакеты
```
Microsoft.Extensions.Configuration
Microsoft.Extensions.Configuration.Binder
Microsoft.Extensions.Configuration.FileExtensions
Microsoft.Extensions.Configuration.Json
```

6. Формируем подключение ADO.net
https://learn.microsoft.com/ru-ru/dotnet/framework/data/adonet/ado-net-code-examples
https://www.npgsql.org/







