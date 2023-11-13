using APITool.Models;
using Microsoft.EntityFrameworkCore;

namespace APITool
{
    public class JokeContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "JokesDb");
        }

        public DbSet<Joke> Jokes { get; set; }
    }
}
