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
create unique index ix_unique_name on public.regions(name);

ALTER TABLE public.fire_history
ADD CONSTRAINT fire_history_region_id FOREIGN KEY (region_id) REFERENCES public.reqions (id);

create index ix_fire_history_period on public.fire_history(period);
create index ix_fire_history_region_id on public.fire_history(region_id);
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


7. Запускаем приложение. Ждем завершаем. Создаем SQL запрос
```sql
select t2.name, count(*) fires from public.fire_history as t1
inner join public.regions as t2 on t1.region_id = t2.id
group by t2.name
order by t2.name
```

Сравниваем результаты с прошлым решением

```bash
|   Регион           |       Кол-во пожаров     |
|--------------------|--------------------------|
| Ангарский район   | 137 |
| Баргузинский район   | 453 |
| Баунтовский район   | 26 |
```






