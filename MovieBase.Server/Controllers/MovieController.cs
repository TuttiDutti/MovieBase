using Microsoft.AspNetCore.Mvc;
using MovieBase.Server.Models;
using MovieBase.Server.Services;

namespace MovieBase.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private TmdbService _tmdbService;
        public MovieController(TmdbService tmdbService) {
            _tmdbService = tmdbService;
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            try
            {
                var result = await _tmdbService.SearchMovieAsync(q);

                var results = result.RootElement.GetProperty("results");

                var movies = results.EnumerateArray().Select(movie => new MovieDto
                {
                    TmdbId = movie.GetProperty("id").GetInt32(),
                    Title = movie.GetProperty("title").GetString() ?? "",
                    Description = movie.GetProperty("overview").GetString() ?? "",
                    PosterUrl = movie.TryGetProperty("poster_path", out var posterPath) && posterPath.ValueKind != System.Text.Json.JsonValueKind.Null
                        ? $"https://image.tmdb.org/t/p/w500{posterPath.GetString()}" : "",
                    ReleaseDate = movie.GetProperty("release_date").GetString() ?? ""
                });

            
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Wystąpił błąd: {ex.Message}");
            }
        }
            
    }
}
