# Проект построения локального стенда
Редакция: 2024-07-04

### Задача
Настроить локальное окружение для сборки приложения. Изучить основы DevOps практики. 

### Действия
1. Скачать и установить локально `Docker`
```bash
 sudo apt-get update
 sudo apt install docker
 sudo apt-get install docker-compose-plugin
```
Проверяем
```bash
sudo docker --version
Docker version 24.0.7, build 24.0.7-0ubuntu2~22.04.1
```

2. Создаем Dockerfile для первого консольного приложения
Официальный образы [Microsoft](https://learn.microsoft.com/ru-ru/dotnet/architecture/microservices/net-core-net-framework-containers/official-net-docker-images)

2. Запускаем сборку 
```bash
sudo docker build -t geoconvertor .
```
3. Проверяем сборку `sudo docker images`

4. Проверяем запуск 
```bash
sudo docker run geoconvertor
Unhandled exception. System.ArgumentException: Необходимо передать аргументы! Наименование файла.
   at GeoJsonConvertor.Program.Main(String[] args) in /build/Program.cs:line 10
   at GeoJsonConvertor.Program.<Main>(String[] args)
```

5. Создаем `docker-compose` файл для запуска приложения.


