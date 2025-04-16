namespace MovieBase.Server.Models
{
    public class Ratings
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public int Score { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
    }
}
