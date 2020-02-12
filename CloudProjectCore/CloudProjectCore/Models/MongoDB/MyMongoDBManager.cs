using CloudProjectCore.Models.Photo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolManager.MongoDB;

namespace CloudProjectCore.Models.MongoDB
{
    public class MyMongoDBManager : MongoDBManager
    {
        public MyMongoDBManager(string connectionString, string databaseName) : base(connectionString, databaseName) { }

        public async Task<List<PhotoModelForGallery>> GetPhotoForGallery(string userId, string tag = "")
        {
            List<PhotoModelForGallery> result = new List<PhotoModelForGallery>();

            CollectionManager<PhotoModel> collectionManager = 
                new CollectionManager<PhotoModel>(database, Variables.MongoDBPhotosCollectionName);
            IAsyncCursor<PhotoModel> response = null;

            if(string.IsNullOrEmpty(tag) || string.IsNullOrWhiteSpace(tag))
                response = await collectionManager.mongoCollection.FindAsync(x => x.UserId == userId);
            else
                response = await collectionManager.mongoCollection.FindAsync(x => x.Tags.Any(y => y.Contains(tag)) && x.UserId == userId);

            response.ToList().ForEach(x => result.Add(new PhotoModelForGallery() { _id = x._id, PhotoPhatPreview = x.PhotoPhatPreview }));

            return result;
        }
    }
}
