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
        public List<string> Tags { get; set; }
        public string PhotoPhatOriginalSize { get; set; }
        public string PhotoPhatPreview { get; set; }     
        public string PhotoTimeOfUpload { get; set; }


        #region EXIF Variables

        // 0x0002
        public double? photoGpsLatitude { get; set; }

        // 0x0004
        public double? photoGpsLongitude { get; set; }

        // 0x0132
        public string photoTagDateTime { get; set; }

        // 0x0100
        public string photoTagImageWidth { get; set; }

        // 0x0101
        public string photoTagImageHeight { get; set; }

        // 0x110
        public string photoTagThumbnailEquipModel { get; set; }

        #endregion
    }
}
