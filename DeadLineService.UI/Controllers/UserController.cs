using Microsoft.AspNetCore.Mvc;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using Microsoft.VisualStudio.Services.Users;
using Newtonsoft.Json;
using System.Text;

namespace DeadLineService.UI.Controllers
{
    public class UserController : Controller
    {

        public async Task<IActionResult> GetUsers()
        {
			IEnumerable<User> result =await HttpRequestForGetUsers("User/GetAllUser");
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
        private async Task<IEnumerable<User>> HttpRequestForGetUsers(string controllerAndMethodName)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost/api/" + controllerAndMethodName);
                if (response.IsSuccessStatusCode)
                {
                    // Успешно принято
                    string responseData = await response.Content.ReadAsStringAsync();
                    List<User>? result = JsonConvert.DeserializeObject<List<User>>(responseData);
                    return result;
                }
                else return Enumerable.Empty<User>();
            }
        }
    }
}
