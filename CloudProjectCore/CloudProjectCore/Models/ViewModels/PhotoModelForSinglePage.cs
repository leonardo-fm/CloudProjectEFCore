using CloudProjectCore.Models.Photo;
using System.ComponentModel.DataAnnotations;

namespace CloudProjectCore.Models.ViewModels
{
    public class PhotoModelForSinglePage : PhotoModel
    {
        public PhotoModelForSinglePage() { }
        public PhotoModelForSinglePage(PhotoModel p)
        {
            _id = p._id;
            ImageName = p.ImageName;
            UserId = p.UserId;
            Description = p.Description;
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

        [Display(Name = "Test")]
        public string LPhotoPhatOriginalSizeWithSasKey { get; set; }
        public string UriForSheredImage { get; set; }
    }
}
