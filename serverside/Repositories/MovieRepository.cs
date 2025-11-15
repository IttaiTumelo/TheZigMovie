using System.Text.Json;
using serverside.Models;

namespace serverside.Repositories
{
    public class MovieRepository(HttpClient httpClient, IConfiguration configuration) : IMovieRepository
    {
        private readonly string _apiKey = configuration["TheMovieDb:ApiKey"];
        // To use the local environment.
        // private readonly string _apiKey = Environment.GetEnvironmentVariable("API_KEY");
        private readonly string _baseUrl = configuration["TheMovieDb:BaseUrl"];

        public async Task<List<Movie>> GetPopularMoviesAsync()
        {
            var url = $"{_baseUrl}/movie/popular?api_key={_apiKey}&language=en-US&page=1";
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<MovieApiResponse>(json, options);
            
            // Return top 20 movies
            return result?.Results?.Take(20).ToList() ?? new List<Movie>();
        }

        public async Task<List<Movie>> SearchMoviesAsync(string query)
        {
            var url = $"{_baseUrl}/search/movie?api_key={_apiKey}&language=en-US&query={Uri.EscapeDataString(query)}&page=1";
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, 
                
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var result = JsonSerializer.Deserialize<MovieApiResponse>(json, options);
            
            return result?.Results ?? new List<Movie>();
        }

        public async Task<MovieDetails> GetMovieByIdAsync(int id)
        {
            var url = $"{_baseUrl}/movie/{id}?api_key={_apiKey}&language=en-US";
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<MovieDetails>(json, options);
            
            return result;
        }
    }
}