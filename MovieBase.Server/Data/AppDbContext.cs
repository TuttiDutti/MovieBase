using Microsoft.EntityFrameworkCore;
using MovieBase.Server.Models;
using System.Collections.Generic;

namespace MovieBase.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<FavoriteMovie> FavoriteMovies { get; set; }

    }
}
