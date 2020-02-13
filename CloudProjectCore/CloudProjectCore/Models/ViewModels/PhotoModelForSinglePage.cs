using CloudProjectCore.Models.Photo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudProjectCore.Models.ViewModels
{
    public class PhotoModelForSinglePage : PhotoModel
    {
        public PhotoModelForSinglePage(){ }
        public PhotoModelForSinglePage(PhotoModel p)
        {
            _id = p._id;
            ImageName = p.ImageName;
            UserId = p.UserId;
            Tags = p.Tags;
            PhotoPhatOriginalSize = p.PhotoPhatOriginalSize;
            PhotoPhatPreview = p.PhotoPhatPreview;
            PhotoTimeOfUpload = p.PhotoTimeOfUpload;
            PhotoGpsLatitude = p.PhotoGpsLatitude;
            PhotoGpsLongitude = p.PhotoGpsLongitude;
            PhotoTagDateTime = p.PhotoTagDateTime;
            PhotoTagImageWidth = p.PhotoTagImageWidth;
            PhotoTagImageHeight = p.PhotoTagImageHeight;
            PhotoTagThumbnailEquipModel = p.PhotoTagThumbnailEquipModel;
    }

        public string LPhotoPhatOriginalSizeWithSasKey { get; set; }
    }
}
