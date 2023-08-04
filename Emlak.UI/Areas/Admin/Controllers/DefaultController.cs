using Microsoft.AspNetCore.Mvc;

namespace Emlak.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DefaultController : Controller
    {

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
