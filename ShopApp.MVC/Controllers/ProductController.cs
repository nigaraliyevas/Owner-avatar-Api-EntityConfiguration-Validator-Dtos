using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApp.MVC.ViewModels.Product;
using System.Net.Http.Headers;

namespace ShopApp.MVC.Controllers
{
    public class ProductController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", Request.Cookies["token"]);
            HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:5159/api/Product?page=1");
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ProductListDto>(data);
                return View(result);
            }
            return BadRequest();
        }
    }
}
