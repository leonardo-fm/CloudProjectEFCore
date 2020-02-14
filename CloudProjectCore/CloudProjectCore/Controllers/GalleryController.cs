using System.Security.Claims;
using CloudProjectCore.Models;
using CloudProjectCore.Models.BlobStorage;
using CloudProjectCore.Models.MongoDB;
using CloudProjectCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudProjectCore.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        private readonly MyMongoDBManager _myMongoDbManager;
        private readonly string _userId;

        public GalleryController(MyMongoDBManager myMongoDBManager, IHttpContextAccessor contextAccessor)
        {
            _myMongoDbManager = myMongoDBManager;
            _userId = contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public IActionResult Gallery(string tag = "", bool multipleDeletes = false)
        {
            var photos = _myMongoDbManager.GetPhotoForGallery(_userId, tag);
            GalleryModel galleryModel = null;

            using (MyBlobStorageManager myBlobStorageManager = new MyBlobStorageManager(Variables.BlobStorageConnectionString, _userId))
            {
                string sasUri = myBlobStorageManager.GetContainerSasUri();

                photos.Result.ForEach(x => x.PhotoPhatPreview += sasUri);

                galleryModel = new GalleryModel()
                {
                    Photos = photos.Result,
                    LastTag = tag,
                    MultipleDeletes = multipleDeletes
                };
            }

            return View(galleryModel);
        }
    }
}