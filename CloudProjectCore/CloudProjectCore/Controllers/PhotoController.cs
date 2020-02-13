using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CloudProjectCore.Models;
using CloudProjectCore.Models.BlobStorage;
using CloudProjectCore.Models.MongoDB;
using CloudProjectCore.Models.Photo;
using CloudProjectCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ToolManager.MongoDB;

namespace CloudProjectCore.Controllers
{
    [Authorize]
    public class PhotoController : Controller
    {
        private readonly MyMongoDBManager _myMongoDbManager;

        public PhotoController(MyMongoDBManager myMongoDBManager)
        {
            _myMongoDbManager = myMongoDBManager;
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeletePhotos([Bind("_id", "ToBeDelete")]List<PhotoModelForDelete> photos)
        {
            MyMongoDBManager myMongoDBManager = new MyMongoDBManager(Variables.MongoDBConnectionStringRW, Variables.MongoDBDatbaseName); 
            CollectionManager<PhotoModel> collectionManager =
                new CollectionManager<PhotoModel>(myMongoDBManager.database, Variables.MongoDBPhotosCollectionName);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            MyBlobManager myBlobManager = new MyBlobManager(Variables.BlobStorageConnectionString, userId.Value);

            List<string> blobsReferenceName = new List<string>();

            foreach (var photo in photos)
                if (photo.ToBeDelete)
                {
                    var photosName = await _myMongoDbManager.GetPhotosName(new ObjectId(photo._id));
                    blobsReferenceName.AddRange(photosName);
                    var response = await collectionManager.RemoveDocumentAsync(new ObjectId(photo._id));
                    //if(response.IsSuccess)
                    //    // log ok
                    //else
                    //    // log no
                }

            foreach (var name in blobsReferenceName)
            {
                var response = await myBlobManager.RemoveDocumentByNameAsync(name);
                //if(response.IsSuccess)
                //    // log ok
                //else
                //    // log no
            }

            return RedirectToAction("Gallery", "Gallery");
        }

        public async Task<IActionResult> SinglePhoto(string photoId)
        {
            ObjectId _id = new ObjectId(photoId);

            CollectionManager<PhotoModel> collectionManager = 
                new CollectionManager<PhotoModel>(_myMongoDbManager.database, Variables.MongoDBPhotosCollectionName);

            var photoResponse = await collectionManager.GetDocumentByIdAsync(_id);
            var photo = new PhotoModelForSinglePage(photoResponse);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            MyBlobManager myBlobManager = new MyBlobManager(Variables.BlobStorageConnectionString, userId.Value);

            photo.LPhotoPhatOriginalSizeWithSasKey = photo.PhotoPhatOriginalSize + myBlobManager.GetContainerSasUri();

            return View(photo);
        }
    }
}