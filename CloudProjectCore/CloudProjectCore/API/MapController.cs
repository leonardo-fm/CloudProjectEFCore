using System.Security.Claims;
using CloudProjectCore.Models;
using CloudProjectCore.Models.BlobStorage;
using CloudProjectCore.Models.MongoDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

                return JsonConvert.SerializeObject(photosForMap.Result);
            }
        }
    }
}