using APITool.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace APITool.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new List<Joke>());
        }

        public async Task<IActionResult> GetJokeAsync()
        {
            List<Joke> jokes = new List<Joke>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://official-joke-api.appspot.com/random_joke"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    jokes.Add(JsonConvert.DeserializeObject<Joke>(apiResponse));
                }
            }
            return View("Index", jokes);
        }

        public async Task<IActionResult> GetTenJokesAsync()
        {
            List<Joke> jokes = new List<Joke>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://official-joke-api.appspot.com/random_ten"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    jokes = JsonConvert.DeserializeObject<List<Joke>>(apiResponse);
                }
            }
            return View("Index", jokes);
        }

        public async Task<IActionResult> GetJokeByTypeAsync(string type)
        {
            List<Joke> jokes = new List<Joke>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://official-joke-api.appspot.com/jokes/" + type + "/random"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    jokes = JsonConvert.DeserializeObject<List<Joke>>(apiResponse);
                }
            }
            return View("Index", jokes);
        }

        public async Task<IActionResult> GetJokeByIDAsync(string id)
        {
            List<Joke> jokes = new List<Joke>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://official-joke-api.appspot.com/jokes/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    jokes.Add(JsonConvert.DeserializeObject<Joke>(apiResponse));
                }
            }
            return View("Index", jokes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}