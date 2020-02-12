using System.Security.Claims;
using CloudProjectCore.Models;
using CloudProjectCore.Models.BlobStorage;
using CloudProjectCore.Models.MongoDB;
using CloudProjectCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudProjectCore.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        private readonly MyMongoDBManager _myMongoDbManager;

        public GalleryController(MyMongoDBManager myMongoDBManager)
        {
            _myMongoDbManager = myMongoDBManager;
        }

        public IActionResult Gallery(string tag = "", bool multipleDeletes = false)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            var photos = _myMongoDbManager.GetPhotoForGallery(userId.Value, tag);

            MyBlobManager blobManager = new MyBlobManager(Variables.BlobStorageConnectionString, userId.Value);
            string sasUri = blobManager.GetContainerSasUri();

            photos.Result.ForEach(x => x.PhotoPhatPreview += sasUri);

            GalleryModel galleryModel = new GalleryModel() { 
                photos = photos.Result, 
                lastTag = tag, 
                multipleDeletes = multipleDeletes 
            };

            return View(galleryModel);
        }
    }
}