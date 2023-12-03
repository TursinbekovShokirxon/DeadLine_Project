using Microsoft.AspNetCore.Mvc;

namespace DeadLineService.UI.Controllers
{

    public class MainPageController : Controller
    {
        public IActionResult MainPage()
        {

            return View();
        }

        public IActionResult UserIndex()
        {

            return View();
        }

    }
}
