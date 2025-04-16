namespace MovieBase.Server.Models
{
    public class FavoriteMovie
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
