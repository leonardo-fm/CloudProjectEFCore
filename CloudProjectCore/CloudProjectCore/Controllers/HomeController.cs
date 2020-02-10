using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CloudProjectCore.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using CloudProjectCore.Models.ViewModels;
using System.Collections.Generic;
using System.IO;

namespace CloudProjectCore.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly List<string> _extensionsPermitted = new List<string>()
        {
            ".jpg", ".png", ".jpeg", ".gif", ".raw", ".bmp"
        };

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
            if (!_extensionsPermitted.Contains(Path.GetExtension(uploadModel.File.FileName)))
            {
                string extensions = "";
                foreach (string ex in _extensionsPermitted)
                    extensions += ex + " ";
                return RedirectToAction("UploadPhotos", new UploadModel()
                { Message = $"The permitted extensions are: {extensions}" }
                );
            }

            if (uploadModel.File.Length == 0)
                return RedirectToAction("UploadPhotos", new UploadModel()
                { Message = $"The file is empty" }
                );
            else if (uploadModel.File.Length > 5000000)
                return RedirectToAction("UploadPhotos", new UploadModel()
                { Message = $"The file is too big, max 5 MB, your file is {System.Math.Round(uploadModel.File.Length / 1000000.0, 2)} MB" }
                );

            return RedirectToAction("UploadPhotos");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
