using Microsoft.AspNetCore.Mvc;

namespace DeadLineService.UI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult GetUsers()
        {

            return View();
        }
    }
}
