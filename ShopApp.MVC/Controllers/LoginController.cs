using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApp.MVC.ViewModels.Login;
using System.Text;

namespace ShopApp.MVC.Controllers
{
    public class LoginController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            using HttpClient client = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(new { username, password }), Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await client.PostAsync("http://localhost:5159/api/Auth/Login", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<UserTokenDto>(data);
                Response.Cookies.Append("token", result.Token);
            }
            return View();
        }
    }
}
