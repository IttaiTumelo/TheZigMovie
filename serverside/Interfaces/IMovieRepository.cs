using serverside.Models;
namespace serverside.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetPopularMoviesAsync();
        Task<List<Movie>> SearchMoviesAsync(string query);
        Task<MovieDetails> GetMovieByIdAsync(int id);
    }
}