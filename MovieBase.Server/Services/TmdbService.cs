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
    }
}
