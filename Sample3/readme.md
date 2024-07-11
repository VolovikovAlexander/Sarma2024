# Пример работы с базой данных

1. Запускаем контейнеры
```bash
sudo docker-compose up -d
```

2. Создаем базу данных
```sql
create unique index ix_unique_name on public.reqions(name);

ALTER TABLE public.fire_history
ADD CONSTRAINT fire_history_region_id FOREIGN KEY (region_id) REFERENCES public.reqions (id);
```
