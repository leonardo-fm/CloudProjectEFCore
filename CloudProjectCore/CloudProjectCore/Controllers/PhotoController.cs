using System.Collections.Generic;
using CloudProjectCore.Models.MongoDB;
using CloudProjectCore.Models.Photo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Delete(List<PhotoModelForGallery> photos)
        {
            foreach (var photo in photos)
            {

            }

            return View();
        }
    }
}