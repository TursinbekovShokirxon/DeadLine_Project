using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.Users;
using Newtonsoft.Json;
using System.Text;

namespace DeadLineService.UI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult GetUsers()
        {
            var result = HttRequestForGetUsers("User/GetAll");
            return View("~/Views/User/UserIndex.cshtml", result);
        }
        private async Task<bool> HttpRequetForPost(object obj, string controllerAndMethodName)
        {
            string jsonData = JsonConvert.SerializeObject(obj);
            using (HttpClient client = new HttpClient())
            {
                using (HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = await client.PostAsync("https://localhost/api/" + controllerAndMethodName, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        await Console.Out.WriteLineAsync(responseData);
                        return true;
                    }
                    else return false;
                }
            }
        }
        private async Task<IEnumerable<User>> HttRequestForGetUsers(string controllerAndMethodName)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost/api/" + controllerAndMethodName);
                if (response.IsSuccessStatusCode)
                {
                    // Успешно принято
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<User>>(responseData);
                }
                else return Enumerable.Empty<User>();
            }
        }




    }
}
