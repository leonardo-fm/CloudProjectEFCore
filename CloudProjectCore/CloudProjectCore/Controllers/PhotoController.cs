using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CloudProjectCore.Models;
using CloudProjectCore.Models.BlobStorage;
using CloudProjectCore.Models.MongoDB;
using CloudProjectCore.Models.Photo;
using CloudProjectCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ToolManager.MongoDB;

namespace CloudProjectCore.Controllers
{
    [Authorize]
    public class PhotoController : Controller
    {
        private readonly MyMongoDBManager _myMongoDbManager;
        private readonly string _userId;

        public PhotoController(MyMongoDBManager myMongoDBManager, IHttpContextAccessor contextAccessor)
        {
            _myMongoDbManager = myMongoDBManager;
            _userId = contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeletePhotos([Bind("_id", "ToBeDelete")]List<PhotoModelForDelete> photos)
        {
            List<string> blobsReferenceName = new List<string>();

            using (
                MyBlobStorageManager myBlobStorageManager = new MyBlobStorageManager(Variables.BlobStorageConnectionString, _userId))
            {
                foreach (var photo in photos)
                    if (photo.ToBeDelete)
                    {
                        var photosName = await _myMongoDbManager.GetPhotosNameAsync(new ObjectId(photo._id));
                        blobsReferenceName.AddRange(photosName);
                        _myMongoDbManager.RemovePhotoAsync(new ObjectId(photo._id));
                    }

                foreach (var name in blobsReferenceName)
                    await myBlobStorageManager.RemoveDocumentByNameAsync(name);
            }

            return RedirectToAction("Gallery", "Gallery");
        }

        public async Task<IActionResult> SinglePhoto(string photoId, string UriForSheredImage = "")
        {
            ObjectId _id = new ObjectId(photoId);
            PhotoModelForSinglePage photo;

            var photoResponse = await _myMongoDbManager.GetPhotoAsync(_id);

            if (photoResponse == null)
                return Content("Wrong parameter");

            photo = new PhotoModelForSinglePage(photoResponse);
            photo.UriForSheredImage = UriForSheredImage;

            using (MyBlobStorageManager myBlobStorageManager = new MyBlobStorageManager(Variables.BlobStorageConnectionString, _userId))
            {
                photo.LPhotoPhatOriginalSizeWithSasKey = photo.PhotoPhatOriginalSize + myBlobStorageManager.GetContainerSasUri();
            }

            return View(photo);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult SharePhoto(SharePhotoModel sharePhotoModel)
        {
            int totalMinutes = (int)sharePhotoModel.DateOfExpire.Subtract(DateTime.Now).TotalMinutes;

            using (MyBlobStorageManager myBlobStorageManager = new MyBlobStorageManager(Variables.BlobStorageConnectionString, _userId))
            {
                sharePhotoModel.UriForSheredImage = sharePhotoModel.PhotoUri + myBlobStorageManager.GetContainerSasUri(totalMinutes);
            }

            return RedirectToAction("SinglePhoto", new { photoId = sharePhotoModel._id, sharePhotoModel.UriForSheredImage });
        }

        public IActionResult GetBlobDownload(PhotoModelForDownload photo)
        {
            using (MyBlobStorageManager myBlobStorageManager = new MyBlobStorageManager(Variables.BlobStorageConnectionString, _userId))
            {
                photo.Link += myBlobStorageManager.GetContainerSasUri();

                var net = new System.Net.WebClient();
                var data = net.DownloadData(photo.Link);
                var content = new System.IO.MemoryStream(data);
                var contentType = "APPLICATION/octet-stream";
                var fileName = photo.OriginalName;
                return File(content, contentType, fileName);
            }
        }
    }
}