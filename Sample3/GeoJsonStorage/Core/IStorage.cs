
namespace GeoJsonConvertor.Core;

public interface IStorage: IErrorHandler
{
    public Task Add<T>(T source) where T:IModel;
}
