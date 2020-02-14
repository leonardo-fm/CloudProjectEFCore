using MongoDB.Bson;
using System.Collections.Generic;
using ToolManager.MongoDB;

namespace CloudProjectCore.Models.Photo
{
    public class PhotoModel : IMongoDocument
    {
        public ObjectId _id { get; set; }
        public string ImageName { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public string PhotoPhatOriginalSize { get; set; }
        public string PhotoPhatPreview { get; set; }     
        public string PhotoTimeOfUpload { get; set; }

        #region EXIF Variables

        // 0x0002
        public double? PhotoGpsLatitude { get; set; }

        // 0x0004
        public double? PhotoGpsLongitude { get; set; }

        // 0x0132
        public string PhotoTagDateTime { get; set; }

        // 0x0100
        public string PhotoTagImageWidth { get; set; }

        // 0x0101
        public string PhotoTagImageHeight { get; set; }

        // 0x110
        public string PhotoTagThumbnailEquipModel { get; set; }

        #endregion
    }
}
