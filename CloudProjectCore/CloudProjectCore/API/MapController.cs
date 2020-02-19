using System.Security.Claims;
using CloudProjectCore.Models;
using CloudProjectCore.Models.BlobStorage;
using CloudProjectCore.Models.MongoDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace CloudProjectCore.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MapController : ControllerBase
    {
        [AutoValidateAntiforgeryToken]
        [Route("GetPhotos")]
        public string GetPhotosForTheMap()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            using (MyMongoDBManager myMongoDBManager =
                new MyMongoDBManager(Variables.MongoDBConnectionStringRW, Variables.MongoDBDatbaseName))
            {
                var photosForMap = myMongoDBManager.GetPhotosForMapAsync(id);
                photosForMap.Wait();

                return JsonConvert.SerializeObject(photosForMap.Result);
            }
        }

        [AutoValidateAntiforgeryToken]
        [Route("GetPhoto")]
        public string GetPhotoForTheMap(string photoId)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            using (MyMongoDBManager myMongoDBManager =
                new MyMongoDBManager(Variables.MongoDBConnectionStringRW, Variables.MongoDBDatbaseName))
            {
                var photoForMap = myMongoDBManager.GetPhotoForMapAsync(id, new ObjectId(photoId));
                photoForMap.Wait();

                return JsonConvert.SerializeObject(photoForMap.Result);
            }
        }
    }
}