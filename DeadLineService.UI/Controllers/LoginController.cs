using Domain.Models.Authtification;
using Infrastructure.Handlers.ForAuthentication;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace DeadLineService.UI.Controllers
{
    public class LoginController : Controller
    {
        
        public async Task<IActionResult> Index([FromForm] UserLoginModel user)
        {
            // peredat API Login https://localhost/api/Auth/Login

            string jsonData = JsonConvert.SerializeObject(user);
            
            using (HttpClient client = new HttpClient())
            {
                // Конфигурируем HttpClient по вашим требованиям (например, устанавливаем тайм-ауты, хедеры и т.д.)
                // 3. Преобразуйте объект в JSON-строку
                // 4. Создайте `HttpContent` с использованием `StringContent`
                using (HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json"))
                {
                    // 5. Выполните POST-запрос
                    HttpResponseMessage response = await client.PostAsync("https://localhost/api/Auth/Login", content);
                    // 6. Обработайте ответ, если необходимоs
                    if (response.IsSuccessStatusCode)
                    {
                        // Успешно отправлено
                        string responseData = await response.Content.ReadAsStringAsync();
                        await Console.Out.WriteLineAsync(responseData);

                        // Ваш код обработки ответа
                    }
                    else
                    {
                        // Ошибка отправки
                        // Обработка ошибки, если необходимо
                    }
                }
            }

            return View("~/Views/Home/Privacy.cshtml");
        }
    }
}
