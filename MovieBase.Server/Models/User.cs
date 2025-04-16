namespace MovieBase.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<FavoriteMovie> FavoriteMovies { get; set; } = new();
        public List<Ratings> Ratings { get; set; } = new();

    }
}
