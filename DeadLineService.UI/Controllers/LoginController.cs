using Infrastructure.Handlers.ForAuthentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace DeadLineService.UI.Controllers
{
    public class LoginController : Controller
    {

        public async Task<IActionResult> Login([FromForm] UserLoginModel user)
        {
            // peredat API Login https://localhost/api/Auth/Login

            var result = await HttpRequet(user, "Auth/Login");
            if (result) return View("~/Views/MainPage/MainPageIndex.cshtml");
            return View("~/Views/Home/Privacy.cshtml");
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration([FromForm] UserRegirstrationModel user)
        {
            var result = await HttpRequet(user, "Auth/Registration");
            if (result) return View("~/Views/MainPage/MainPageIndex.cshtml");
            return View("~/Views/Home/Privacy.cshtml");
        }

        private async Task<bool> HttpRequet(object obj, string controllerAndMethodName)
        {
            string jsonData = JsonConvert.SerializeObject(obj);
            using (HttpClient client = new HttpClient())
            {
                using (HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = await client.PostAsync("https://localhost/api/" + controllerAndMethodName, content);
                    if (response.IsSuccessStatusCode)
                    {
                        // Успешно отправлено
                        string responseData = await response.Content.ReadAsStringAsync();
                        await Console.Out.WriteLineAsync(responseData);
                        return true;
                    }
                    else return false;
                }
            }
        }
    }
}
