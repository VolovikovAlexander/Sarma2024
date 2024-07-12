namespace GeoJsonConvertor.Extensions;
public static class QuerableExtension
{
    /// <summary>
    /// Наложить отбор при выполнении условия
    /// </summary>
    /// <typeparam name="T"> Любой тип данных </typeparam>
    /// <param name="source"> Исходный набор значений </param>
    /// <param name="condition"> Условие </param>
    /// <param name="predicate"> Отбор </param>
    /// <returns> IQueryable<T> </returns> 
    public static IQueryable<T> IfWhere<T>(
                this IEnumerable<T> source, 
                Func<bool> condition,
                System.Linq.Expressions.Expression<Func<T, bool>> predicate) 
                => condition() ? source.AsQueryable().Where(predicate) : source.AsQueryable();
}
