using APITool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;


namespace APITool.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class APIController : ControllerBase
    {
        private readonly JokeContext _dbcontext;

        public APIController(JokeContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //Adding API calls below


        /// <summary>
        /// Gets all jokes.
        /// </summary>
        [SwaggerOperation(Summary = "Gets all jokes.")]
        [Route("/get")]
        [HttpGet("")]
        public IEnumerable<Joke> RetrieveAllJokes()
        {
            return _dbcontext.Jokes.ToList();
        }

        /// <summary>
        /// Get a joke by id.
        /// </summary>
        /// <param name="ID">The ID of the joke to retrieve.</param>
        [SwaggerOperation(Summary = "Get a joke by id.")]
        [Route("/getID")]
        [HttpGet("{ID}")]
        public IEnumerable<Joke> RetrieveJokeByID(int id)
        {
            return _dbcontext.Jokes.Where(x => x.Id == id);
        }

        /// <summary>
        /// Create a joke.
        /// </summary>
        /// <param name="ID">The ID of the joke.</param>
        /// <param name="Type">The genre of the joke.</param>
        /// <param name="Setup">The opening of the joke.</param>
        /// <param name="Punchline">The funny part of the joke.</param>
        [SwaggerOperation(Summary = "Create a joke.")]
        [Route("/create")]
        [HttpPost]
        public void CreateJoke(int id, string type, string setup, string punchline)
        {
            var joke = new Joke(id, type, setup, punchline);
            _dbcontext.Jokes.Add(joke);
            _dbcontext.SaveChanges();
        }

        /// <summary>
        /// Edit a joke.
        /// </summary>
        /// <param name="ID">The ID of the joke.</param>
        /// <param name="Type">The genre of the joke.</param>
        /// <param name="Setup">The opening of the joke.</param>
        /// <param name="Punchline">The funny part of the joke.</param>
        [SwaggerOperation(Summary = "Edit a joke.")]
        [Route("/edit")]
        [HttpPost]
        public void EditJoke(int id, string type, string setup, string punchline)
        {
            var joke = _dbcontext.Jokes.FirstOrDefault(x => x.Id == id);
            if (joke != null)
            {
                joke.Type = type;
                joke.Setup = setup;
                joke.Punchline = punchline;
                _dbcontext.SaveChanges();
            }
        }
    }
}
