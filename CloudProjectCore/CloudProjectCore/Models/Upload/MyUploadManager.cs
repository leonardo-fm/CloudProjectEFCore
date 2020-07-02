using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using CloudProjectCore.Models.Photo;
using ToolManager.ComputerVision;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using ToolManager.BlobStorage;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Drawing;
using System.Linq;
using ToolManager;
using System.Text;
using System.IO;
using System;
using MongoDB.Bson;
using CloudProjectCore.Models.MongoDB;
using System.Drawing.Drawing2D;

namespace CloudProjectCore.Models.Upload
{
    public static class MyUploadManager
    {
        public static async Task<Responses> UploadNewPhoto(IFormFile file, string userId)
        {
            Stream photoForBlobStorageOriginalSize = new MemoryStream();
            Stream photoForBlobStoragePreview = new MemoryStream();
            Stream photoForBlobStorageIcon = new MemoryStream();
            Stream photoForComputerVision = new MemoryStream();
            Stream photoStream = new MemoryStream();

            using (Stream photo = file.OpenReadStream())
            {
                photo.CopyStream(photoForBlobStorageOriginalSize);
                photo.CopyStream(photoForComputerVision);
                photo.CopyStream(photoStream);
            }

            var imageModifier = new ImageModifier();
            var image = Image.FromStream(photoStream);
            imageModifier.MakePreview(image, photoForBlobStoragePreview, new Size(300, 169), new Size(300, 169));

            var exifDataExtractor = new ExifDataExtractor();
            var exifDataResponse = exifDataExtractor.GetExifDataFromImage(image);

            Task<PhotoResponseForBlobStorage> iconImageUploadData = null;
            if (exifDataResponse.PhotoGpsLatitude != null && exifDataResponse.PhotoGpsLongitude != null)
            {
                imageModifier.MakePreview(image, photoForBlobStorageIcon, new Size(90, 77), new Size(100, 100));

                var imageIcon = (Bitmap)Image.FromStream(photoForBlobStorageIcon);
                var windowIcon = (Bitmap)Image.FromFile("Data/Icons/IconWindow.png");

                photoForBlobStorageIcon.Seek(0, SeekOrigin.Begin);
                imageModifier.GetTheImageIconForMaps(windowIcon, imageIcon).Save(photoForBlobStorageIcon, ImageFormat.Png);
                photoForBlobStorageIcon.Seek(0, SeekOrigin.Begin);

                iconImageUploadData = UploadPhotoToBlobStorageAsync
                    (photoForBlobStorageIcon, GetNewNameForStorage(".png"), userId);
            }

            var computerVisionresoult = GetTagsAsync(photoForComputerVision);

            var originalImageUploadData = UploadPhotoToBlobStorageAsync
                (photoForBlobStorageOriginalSize, GetNewNameForStorage(Path.GetExtension(file.FileName)), userId);

            var previewImageUploadData = UploadPhotoToBlobStorageAsync
                (photoForBlobStoragePreview, GetNewNameForStorage(".png"), userId);

            if (iconImageUploadData != null)
                await Task.WhenAll(computerVisionresoult, originalImageUploadData, previewImageUploadData, iconImageUploadData);
            else
                await Task.WhenAll(computerVisionresoult, originalImageUploadData, previewImageUploadData);


            PhotoModel photoModel = new PhotoModel
            {
                _id = new ObjectId(),
                UserId = userId,
                ImageName = file.FileName,
                Tags = computerVisionresoult.Result.Tags,
                Description = computerVisionresoult.Result.Description,
                PhotoTimeOfUpload = DateTime.UtcNow.ToString(@"yyyy/MM/dd HH:mm:ss"),
                PhotoPhatOriginalSize = originalImageUploadData.Result.PhotoPhat,
                PhotoPhatPreview = previewImageUploadData.Result.PhotoPhat,
                PhotoPhatIcon = iconImageUploadData != null ? iconImageUploadData.Result.PhotoPhat : "",
                PhotoGpsLatitude = exifDataResponse.PhotoGpsLatitude,
                PhotoGpsLongitude = exifDataResponse.PhotoGpsLongitude,
                PhotoTagImageWidth = exifDataResponse.PhotoTagImageWidth,
                PhotoTagImageHeight = exifDataResponse.PhotoTagImageHeight,
                PhotoTagDateTime = exifDataResponse.PhotoTagDateTime,
                PhotoTagThumbnailEquipModel = exifDataResponse.PhotoTagThumbnailEquipModel
            };

            return await UploadNewPhotoOnMongoDBAsync(photoModel);
        }

        private static void CopyStream(this Stream stream, Stream streamDestination)
        {
            if (stream.Position != 0)
                stream.Seek(0, SeekOrigin.Begin);

            stream.CopyTo(streamDestination);
            streamDestination.Seek(0, SeekOrigin.Begin);
            stream.Seek(0, SeekOrigin.Begin);
        }
        private static async Task<ComputerVisionModelData> GetTagsAsync(Stream photoToBeElaborate)
        {
            var featureList = new List<VisualFeatureTypes>()
            {
                VisualFeatureTypes.Description,
                VisualFeatureTypes.Tags
            };

            using (ComputerVisionManager computerVision =
                new ComputerVisionManager(Variables.ComputerVisionEndpoint, Variables.ComputerVisionKey, featureList))
            {
                return await computerVision.GetDataFromImageAsync(photoToBeElaborate);
            }
        }
        private static async Task<PhotoResponseForBlobStorage> UploadPhotoToBlobStorageAsync
            (Stream photo, string photoName, string userId)
        {
            PhotoResponseForBlobStorage photoResponse = new PhotoResponseForBlobStorage();

            using (BlobStorageManager blobStorageManager = new BlobStorageManager(Variables.BlobStorageConnectionString, userId))
            {
                var response = await blobStorageManager.AddDocumentAsync(photo, photoName);
                if (response.IsSuccess)
                    photoResponse.PhotoPhat = blobStorageManager.GetDocumentPath(photoName);
            }

            return photoResponse;
        }
        private static async Task<Responses> UploadNewPhotoOnMongoDBAsync(PhotoModel photo)
        {
            using (MyMongoDBManager myMongoDBManager =
                new MyMongoDBManager(Variables.MongoDBConnectionStringRW, Variables.MongoDBDatbaseName))
            {
                return await myMongoDBManager.AddPhotoAsync(photo);
            }
        }
        private static string GetNewNameForStorage(string fileExtention)
        {
            return Guid.NewGuid().ToString() + fileExtention;
        }
    }
}
