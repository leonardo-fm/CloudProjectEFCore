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
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using System;

namespace CloudProjectCore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> _localizer;

        private readonly List<string> _extensionsPermitted = new List<string>()
        {
            ".jpg", ".png", ".jpeg", ".gif", ".raw", ".bmp"
        };
        private readonly int _fileMaxLengthLimit = 5 * 1024 * 1024;
        private readonly string _userId;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor, IStringLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            _userId = contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public IActionResult Index()
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
                return RedirectToAction("UploadPhotos", new UploadModel { Message = _localizer["File not suported"] });

            var response = await MyUploadManager.UploadNewPhoto(uploadModel.File, _userId);

            if (response.IsSuccess)
                return RedirectToAction("UploadPhotos", new UploadModel { Message = _localizer["File uploaded"] });
            else
                return RedirectToAction("UploadPhotos", new UploadModel { Message = _localizer["Internal error :("] });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) }
            );

            return LocalRedirect(returnUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
