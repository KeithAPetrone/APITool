using APITool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;

namespace APITool.Controllers
{
    public class HomeController : Controller
    {
        private readonly JokeContext _dbcontext;

        public HomeController(JokeContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IActionResult> Index()
        {
            return View(_dbcontext.Jokes.ToList());
        }

        public async Task<IActionResult> GetJokeAsync()
        {
            List<Joke> jokes = new List<Joke>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://official-joke-api.appspot.com/random_joke"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var joke = JsonConvert.DeserializeObject<Joke>(apiResponse);
                    if (_dbcontext.Jokes.Any(j => j.Id == joke.Id))
                    {
                        _dbcontext.Jokes.Add(joke);
                        _dbcontext.SaveChanges();
                        jokes.Add(joke);
                    }
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
                    foreach (var joke in jokes)
                    {
                        if (_dbcontext.Jokes.Any(j => j.Id == joke.Id))
                        {
                            _dbcontext.Jokes.Add(joke);
                            _dbcontext.SaveChanges();
                        }
                    }
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
                    foreach (var joke in jokes)
                    {
                        if (_dbcontext.Jokes.Any(j => j.Id == joke.Id))
                        {
                            _dbcontext.Jokes.Add(joke);
                            _dbcontext.SaveChanges();
                        }
                    }
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
                    var joke = JsonConvert.DeserializeObject<Joke>(apiResponse);
                    if (_dbcontext.Jokes.Any(j => j.Id != joke.Id))
                    {
                        _dbcontext.Jokes.Add(joke);
                        _dbcontext.SaveChanges();
                        jokes.Add(joke);
                    }
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