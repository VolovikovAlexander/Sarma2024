namespace GeoJsonWeb;

/// <summary>
/// Набор расширений для модели <see cref="ExpectanceFires"/>
/// </summary>
public static class Expectance
{
    /// <summary>
    /// Сформировать произвольный набор данных по ожиданиям от пожара
    /// </summary>
    /// <param name="startPeriod"> Начало периода </param>
    /// <param name="endPeriod"> Окончание периода </param>
    /// <returns></returns>
    public static IEnumerable<ExpectanceFires> Build(DateTime startPeriod, DateTime endPeriod)
    {
        if(endPeriod <= startPeriod) throw new ArgumentException("Некорректно указан период!");

        var currentPeriod = startPeriod;
        var currentCode = currentPeriod.ToString("yyyyMM");
        
        var rnd = new Random();
        var result = new List<ExpectanceFires>();

        while(currentPeriod < endPeriod)
        {
            result.Add( new ExpectanceFires()
            {
                 Id = currentCode, Value = rnd.Next(0, 100)
            });

            currentPeriod = currentPeriod.AddMonths(1);
            currentCode = currentPeriod.ToString("yyyyMM");
        }

        return result;
    }
}
