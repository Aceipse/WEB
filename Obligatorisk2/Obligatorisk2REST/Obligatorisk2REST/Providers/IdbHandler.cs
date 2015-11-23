using MongoDB.Driver;

namespace Obligatorisk2REST
{
    public interface IdbHandler
    {
        IMongoDatabase getDb();
    }
}