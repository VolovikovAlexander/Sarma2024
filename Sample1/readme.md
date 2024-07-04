# Проект простого консольного приложения
Редакция: 2024-07-01

### Задача
Необходимо сформировать конвертор `geojson` файлов во внутреннюю структуру C# и запись данных в PostgreSQL

### Действия
1. Открывает сервис: [https://json2csharp.com/](https://json2csharp.com/)
2. Добавляем кусок Json кода. Производим конвертацию данных
3. Результат:
```csharp
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Crs
    {
        public string type { get; set; }
        public Properties properties { get; set; }
    }

    public class Feature
    {
        public string type { get; set; }
        public Properties properties { get; set; }
        public Geometry geometry { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public List<List<List<List<double>>>> coordinates { get; set; }
    }

    public class Properties
    {
        public string name { get; set; }
        public string area { get; set; }
        public string IDate { get; set; }
        public string Id { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public DateTime init_date { get; set; }
        public string name_ru { get; set; }
    }

    public class Root
    {
        public string type { get; set; }
        public string name { get; set; }
        public Crs crs { get; set; }
        public List<Feature> features { get; set; }
    }

```

4. Запускаем команду `dotnet new list`
Получим список шаблонов для создания приложений. Нас интересует консольный вариант `console`

```
Console App                                   console             [C#],F#,VB  Common/Console                  
dotnet gitignore file                         gitignore                       Config      
```
 
5. Запускаем:
```
dotnet new gitignore
dotnet new console -n GeoJsonConvertor --use-program-main
```

6. Практическое занятие на разработку консольного приложения.

### Задание
1. Доработать учебный проект. Добавить модели 
- Регион пожара
- Координаты
- Год, Месяц, Дата фиксации

2. Добавить в конвертор новое преобразование.
   - Добавить набор собственных моделей
   - Расширить класс `Convertor` добавить метод, который будет возвращать словарь <Регион, Данные>
   - Регион с данными должен быть связан уникальным ключом.
   - В тело основной программы добавить вывод даты / времени до миллисекунды.



