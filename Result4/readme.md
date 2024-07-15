# Описание решения

1. Формируем выборку данных 
```sql

select 
'insert into public.work_results(metrics_details_id, work_history_id, column, value, row_id)
values(1,1,''year'',' || year::text || ',' || row_id::text || ');' as insert_text
from
(
	select ROW_NUMBER () OVER ( ORDER BY year ) as row_id , year, count(*) as cnt from public.fire_history where region_id = '210785d9-5886-4961-bd02-1ed709b96887'
	group by year
	order by year
)

union all

select 
'insert into public.work_results(metrics_details_id, work_history_id, column, value, row_id)
values(1,1,''cnt'',' || cnt::text || ',' || row_id::text || ');' as insert_text
from
(
	select  ROW_NUMBER () OVER ( ORDER BY year )  as row_id , year, count(*) as cnt from public.fire_history where region_id = '210785d9-5886-4961-bd02-1ed709b96887'
	group by year
	order by year
)
```

2. Вставляем данные в таблицу `public.metrics_details`


