using Microsoft.AspNetCore.Mvc;
using serverside.Models;

namespace serverside.Controllers
{
    [Route("api")]
    [ApiController]
    public class MoviesController(IMovieRepository movieRepository) : ControllerBase
    {
        // Dependency Injection of repository

        // GET: api/popular
        [HttpGet("popular")]
        public async Task<ActionResult<List<Movie>>> GetPopularMovies()
        {
            try
            {
                var movies = await movieRepository.GetPopularMoviesAsync();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/search?query=birdbox
        [HttpGet("search")]
        public async Task<ActionResult<List<Movie>>> SearchMovies([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Query parameter is required");
            }

            try
            {
                var movies = await movieRepository.SearchMoviesAsync(query);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/movie/550
        [HttpGet("movie/{id}")]
        public async Task<ActionResult<MovieDetails>> GetMovieById(int id)
        {
            try
            {
                var movie = await movieRepository.GetMovieByIdAsync(id);
                
                if (movie == null)
                {
                    return NotFound($"Movie with ID {id} not found");
                }
                
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}