using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ToolManager.MongoDB
{
    public class CollectionManager<T> : IDisposable where T : IMongoDocument
    {
        public IMongoCollection<T> mongoCollection;

        public CollectionManager(IMongoDatabase database, string collectionName)
        {
            try
            {
                mongoCollection = database.GetCollection<T>(collectionName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Responses> AddDocumentAsync(T document)
        {
            try
            {
                await mongoCollection.InsertOneAsync(document);
                return new Responses(true, false);
            }
            catch (Exception)
            {
                return new Responses(false, true);
            }
        }
        public async Task<Responses> RemoveDocumentAsync(ObjectId _id)
        {
            try
            {
                await mongoCollection.FindOneAndDeleteAsync(x => x._id == _id);
                return new Responses(true, false);
            }
            catch (Exception)
            {
                return new Responses(false, true);
            }
        }
        public async Task<T> GetDocumentByIdAsync(ObjectId _id)
        {
            try
            {
                return await mongoCollection.Find(x => x._id == _id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public void Dispose()
        {
            mongoCollection = null;
        }
    }
}
