namespace MovieBase.Server.Models
{
    public class MovieListDto
    {
        public int TmdbId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public List<string> Genres { get; set; } = new();
    }
}
