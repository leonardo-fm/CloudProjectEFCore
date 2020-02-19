using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudProjectCore.Controllers
{
    [Authorize]
    public class MapController : Controller
    {
        public IActionResult Map()
        {
            return View();
        }
    }
}