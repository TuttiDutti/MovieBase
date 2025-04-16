namespace MovieBase.Server.Models
{
    public class MovieDto
    {
        public int TmdbId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public string ReleaseDate { get; set; } = string.Empty;
    }
}
