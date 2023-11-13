namespace APITool.Models
{
    public class Joke
    {
        public Joke(int id, string type, string setup, string punchline)
        {
            Id = id;
            Type = type;
            Setup = setup;
            Punchline = punchline;
        }

        public string? Type { get; set; }
        public string? Setup { get; set; }
        public string? Punchline { get; set; }
        public int Id { get; set; }
    }
}
