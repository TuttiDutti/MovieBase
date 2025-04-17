using Microsoft.AspNetCore.Mvc;
using MovieBase.Server.Models;
using MovieBase.Server.Services;
using System.Text.Json;

namespace MovieBase.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private TmdbService _tmdbService;
        public MovieController(TmdbService tmdbService) {
            _tmdbService = tmdbService;
        }
        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] string? q)
        {
            JsonDocument result;
            var genreDict = await _tmdbService.GetGenresAsync();
            try
            {
                if (string.IsNullOrEmpty(q))
                {
                     result = await _tmdbService.GetPopularMoviesAsync();
                }
                else
                {
                     result = await _tmdbService.SearchMovieAsync(q);
                   
                }
                var results = result.RootElement.GetProperty("results");

                var movies = results.EnumerateArray().Select(movie => new MovieListDto
                {
                    TmdbId = movie.GetProperty("id").GetInt32(),
                    Title = movie.GetProperty("title").GetString() ?? "",
                    PosterUrl = movie.TryGetProperty("poster_path", out var posterPath) && posterPath.ValueKind != System.Text.Json.JsonValueKind.Null
                        ? $"https://image.tmdb.org/t/p/w500{posterPath.GetString()}" : "",
                    Genres = movie.TryGetProperty("genre_ids", out var genreArray) && genreArray.ValueKind == JsonValueKind.Array 
                        ? genreArray.EnumerateArray()
                        .Select(x => genreDict.ContainsKey(x.GetInt32()) ? genreDict[x.GetInt32()] : "")
                        .Where(g => !string.IsNullOrEmpty(g))
                        .ToList() :new List<string>()
                });


                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Wystąpił błąd: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var result = await _tmdbService.GetMovieAsync(id);
                var genreDict = await _tmdbService.GetGenresAsync();
                var movieJson = result.RootElement;

                var movie = new MovieDto
                {
                    TmdbId = movieJson.GetProperty("id").GetInt32(),
                    Title = movieJson.GetProperty("title").GetString() ?? "",
                    Description = movieJson.GetProperty("overview").GetString() ?? "",
                    PosterUrl = movieJson.TryGetProperty("poster_path", out var posterPath) && posterPath.ValueKind != JsonValueKind.Null
                        ? $"https://image.tmdb.org/t/p/w500{posterPath.GetString()}"
                        : "",
                    ReleaseDate = movieJson.GetProperty("release_date").GetString() ?? "",
                    Runtime = movieJson.GetProperty("runtime").GetInt32(),
                    Genres = movieJson.TryGetProperty("genres", out var genreArray) && genreArray.ValueKind == JsonValueKind.Array
                        ? genreArray.EnumerateArray()
                            .Select(g => g.GetProperty("name").GetString() ?? "")
                            .Where(name => !string.IsNullOrWhiteSpace(name))
                            .ToList()
                        : new List<string>()
                };

                return Ok(movie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Wystąpił błąd: {ex.Message}");
            }
        }
            
    }
}
