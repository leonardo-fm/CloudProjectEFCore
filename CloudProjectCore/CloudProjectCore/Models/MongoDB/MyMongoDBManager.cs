using CloudProjectCore.Models.BlobStorage;
using CloudProjectCore.Models.Photo;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ToolManager;
using ToolManager.MongoDB;

namespace CloudProjectCore.Models.MongoDB
{
    public class MyMongoDBManager : MongoDBManager
    {
        public MyMongoDBManager(string connectionString, string databaseName) : base(connectionString, databaseName) { }

        public async Task<List<string>> GetPhotosNameAsync(ObjectId _id)
        {
            using (CollectionManager<PhotoModel> collectionManager =
                new CollectionManager<PhotoModel>(database, Variables.MongoDBPhotosCollectionName))
            {
                var photoObject = await collectionManager.mongoCollection.Find(x => x._id == _id).FirstOrDefaultAsync();
                string nameOriginal = Path.GetFileName(photoObject.PhotoPhatOriginalSize);
                string namePreview = Path.GetFileName(photoObject.PhotoPhatPreview);

                return new List<string>() { nameOriginal, namePreview };
            }
        }
        public async Task<List<PhotoModelForGallery>> GetPhotoForGalleryAsync(string userId, string tag = "")
        {
            List<PhotoModelForGallery> result = new List<PhotoModelForGallery>();

            using (CollectionManager<PhotoModel> collectionManager =
                new CollectionManager<PhotoModel>(database, Variables.MongoDBPhotosCollectionName))
            {
                IAsyncCursor<PhotoModel> response = null;

                if (string.IsNullOrEmpty(tag) || string.IsNullOrWhiteSpace(tag))
                    response = await collectionManager.mongoCollection.FindAsync(x => x.UserId == userId);
                else
                    response = await collectionManager.mongoCollection.FindAsync(x => x.Tags.Any(y => y.Equals(tag)) 
                    && x.UserId == userId);

                response.ToList().ForEach(x => result.Add(new PhotoModelForGallery() { _id = x._id, PhotoPhatPreview = x.PhotoPhatPreview }));
            }

            result.Reverse();
            return result;
        }
        public async void RemovePhotoAsync(ObjectId _id)
        {
            using (CollectionManager<PhotoModel> collectionManager =
                new CollectionManager<PhotoModel>(database, Variables.MongoDBPhotosCollectionName))
            {
                await collectionManager.RemoveDocumentAsync(_id);
            }
        }
        public async Task<Responses> AddPhotoAsync(PhotoModel photo)
        {
            using (CollectionManager<PhotoModel> collectionManager =
                new CollectionManager<PhotoModel>(database, Variables.MongoDBPhotosCollectionName))
            {
                return await collectionManager.AddDocumentAsync(photo);
            }
        }
        public async Task<PhotoModel> GetPhotoAsync(ObjectId _id)
        {
            using (CollectionManager<PhotoModel> collectionManager =
                new CollectionManager<PhotoModel>(database, Variables.MongoDBPhotosCollectionName))
            {
                return await collectionManager.GetDocumentByIdAsync(_id);
            }
        }
        public async Task<List<PhotoModelForMap>> GetPhotosForMapAsync(string id)
        {
            using (CollectionManager<PhotoModel> collectionManager =
                new CollectionManager<PhotoModel>(database, Variables.MongoDBPhotosCollectionName))
            using (MyBlobStorageManager myBlobStorageManager =
                new MyBlobStorageManager(Variables.BlobStorageConnectionString, id))
            {
                var photosForMap = new List<PhotoModelForMap>();

                var res = await collectionManager.mongoCollection.Find(
                    x => x.PhotoGpsLatitude != null 
                    && x.PhotoGpsLongitude != null).ToListAsync();

                var sasKey = myBlobStorageManager.GetContainerSasUri(10);

                foreach(var photo in res)
                {
                    photosForMap.Add(new PhotoModelForMap
                    {
                        _id = photo._id.ToString(),
                        IconPath = photo.PhotoPhatIcon + sasKey,
                        PhotoName = photo.ImageName,
                        PhotoLatitude = photo.PhotoGpsLatitude ?? 0,
                        PhotoLongitude = photo.PhotoGpsLongitude ?? 0
                    });
                }

                return photosForMap;
            }
        }
    }
}
