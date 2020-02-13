using MongoDB.Bson;
using ToolManager.MongoDB;

namespace CloudProjectCore.Models.Photo
{
    public class PhotoModelForGallery : IMongoDocument
    {
        public ObjectId _id { get; set; }
        public string PhotoPhatPreview { get; set; }
        public bool ToBeDelete { get; set; }
    }
}
