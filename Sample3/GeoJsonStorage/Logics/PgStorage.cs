using GeoJsonConvertor.Core;

namespace GeoJsonConvertor.Logics;

/// <summary>
/// Реализация хранилища согласно интерфейсу <see cref="IStorage"/>
/// </summary>
public class PgStorage: AbstractErrorHandler, IStorage
{
    private readonly StorageOptions _options;

    public PgStorage(StorageOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public Task Add<T>(T source) where T : IModel
    {
        throw new NotImplementedException();
    }
}
