using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using TextBeltTest.Models;

namespace TextBeltTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string phoneNumber, string message)
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
        {
            { "phone", phoneNumber },
            { "message", message },
            { "key", "textbelt" }
        };

                var content = new FormUrlEncodedContent(parameters);
                var response = await client.PostAsync("https://textbelt.com/text", content);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.SuccessMessage = "SMS sent successfully!";
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to send SMS.";
                }
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
