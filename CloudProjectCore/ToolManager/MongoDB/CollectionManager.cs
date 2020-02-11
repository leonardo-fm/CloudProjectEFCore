using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ToolManager.MongoDB
{
    public class CollectionManager<T> where T : IMongoDocument
    {
        private IMongoCollection<T> _mongoCollection;

        public CollectionManager(IMongoDatabase database, string collectionName)
        {
            try
            {
                _mongoCollection = database.GetCollection<T>(collectionName);
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
                await _mongoCollection.InsertOneAsync(document);
                return new Responses(true, false, false);
            }
            catch (Exception)
            {
                return new Responses(false, true, false);
            }
        }
        public async Task<Responses> RmoveDocumentAsync(T document)
        {
            try
            {
                await _mongoCollection.FindOneAndDeleteAsync(x => x._id == document._id);
                return new Responses(true, false, false);
            }
            catch (Exception)
            {
                return new Responses(false, true, false);
            }
        }
        public async Task<T> GetDocumentByIdAsync(ObjectId _id)
        {
            try
            {
                return await _mongoCollection.Find(x => x._id == _id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
