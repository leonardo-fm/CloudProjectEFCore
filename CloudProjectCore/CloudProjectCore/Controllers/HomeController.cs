using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CloudProjectCore.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using CloudProjectCore.Models.ViewModels;
using System.Collections.Generic;
using ToolManager.Helpers.UploadHelper;
using CloudProjectCore.Models.Upload;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CloudProjectCore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly List<string> _extensionsPermitted = new List<string>()
        {
            ".jpg", ".png", ".jpeg", ".gif", ".raw", ".bmp"
        };
        private readonly int _fileMaxLengthLimit = 5 * 1024 * 1024;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult UploadPhotos(UploadModel uploadModel = null)
        {
            if (uploadModel == null)
                uploadModel = new UploadModel() { Message = "" };

            return View(uploadModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadPost(UploadModel uploadModel)
        {
            if (uploadModel.File == null
                || !UploadHelper.IsSingleContentType(uploadModel.File)
                || !UploadHelper.HasAValidExtention(uploadModel.File, _extensionsPermitted)
                || !UploadHelper.IsInLengthLimits(uploadModel.File, _fileMaxLengthLimit))
                return RedirectToAction("UploadPhotos", new UploadModel { Message = "File not suported" });

            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            var response = await MyUploadManager.UploadNewPhoto(uploadModel.File, userId.Value);

            return RedirectToAction("UploadPhotos");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
