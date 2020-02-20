using MongoDB.Bson;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToolManager.MongoDB; 

namespace CloudProjectCore.Models.Photo
{
    public class PhotoModel : IMongoDocument
    {
        public ObjectId _id { get; set; }

        [Display(Name = "Name")]
        public string ImageName { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Tags")]
        public List<string> Tags { get; set; }

        public string PhotoPhatOriginalSize { get; set; }

        public string PhotoPhatPreview { get; set; }    
        
        public string PhotoPhatIcon { get; set; }

        [Display(Name = "Time of the upload")]
        public string PhotoTimeOfUpload { get; set; }

        #region EXIF Variables

        // 0x0002
        [Display(Name = "Latitude")]
        public double? PhotoGpsLatitude { get; set; }

        // 0x0004
        [Display(Name = "Longitude")]
        public double? PhotoGpsLongitude { get; set; }

        // 0x0132
        [Display(Name = "Moment of shot")]
        public string PhotoTagDateTime { get; set; }

        // 0x0100
        [Display(Name = "Width")]
        public string PhotoTagImageWidth { get; set; }

        // 0x0101
        [Display(Name = "Height")]
        public string PhotoTagImageHeight { get; set; }

        // 0x110
        [Display(Name = "Equip model")]
        public string PhotoTagThumbnailEquipModel { get; set; }

        #endregion
    }
}
