using MovieBase.Server.Models;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace MovieBase.Server.Services
{
    public class TmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public TmdbService(HttpClient httpClient, IConfiguration configuration) 
        { 
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<JsonDocument> SearchMovieAsync(string query)
        {
            var apiKey = _configuration["Tmdb:ApiKey"];
            var url = $"https://api.themoviedb.org/3/search/movie?query={query}&api_key={apiKey}";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            return JsonDocument.Parse(content);
        }
        public async Task<JsonDocument> GetMovieAsync(int id)
        {
            var apiKey = _configuration["Tmdb:ApiKey"];
            var url = $"https://api.themoviedb.org/3/movie/{id}?api_key={apiKey}";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            return JsonDocument.Parse(content);
        }
        public async Task<JsonDocument> GetPopularMoviesAsync()
        {
            var apiKey = _configuration["Tmdb:ApiKey"];
            var url = $"https://api.themoviedb.org/3/movie/popular?api_key={apiKey}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonDocument.Parse(content);
        }
        public async Task<Dictionary<int, string>> GetGenresAsync()
        {
            var apiKey = _configuration["Tmdb:ApiKey"];
            var url = $"https://api.themoviedb.org/3/genre/movie/list?api_key={apiKey}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(content);

            var genreDict = new Dictionary<int, string>();
            foreach (var genre in json.RootElement.GetProperty("genres").EnumerateArray())
            {
                var id = genre.GetProperty("id").GetInt32();
                var name = genre.GetProperty("name").GetString() ?? "";
                genreDict[id] = name;
            }

            return genreDict;
        }
    }
}
