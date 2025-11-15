using Microsoft.AspNetCore.Mvc;
using Moq;
using serverside.Controllers;
using serverside.Models;
using Xunit;

namespace serverside.Tests
{
    public class MoviesControllerTests
    {
        private readonly Mock<IMovieRepository> _mockRepository;
        private readonly MoviesController _controller;

        public MoviesControllerTests()
        {
            _mockRepository = new Mock<IMovieRepository>();
            _controller = new MoviesController(_mockRepository.Object);
        }

        [Fact]
        public async Task GetPopularMovies_ReturnsOkResult_WithListOfMovies()
        {
            // Arrange
            var mockMovies = new List<Movie>
            {
                new () { Id = 1, Title = "Test Movie 1" },
                new () { Id = 2, Title = "Test Movie 2" }
            };
            _mockRepository.Setup(repo => repo.GetPopularMoviesAsync())
                .ReturnsAsync(mockMovies);

            // Act
            var result = await _controller.GetPopularMovies();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var movies = Assert.IsType<List<Movie>>(okResult.Value);
            Assert.Equal(2, movies.Count);
        }

        [Fact]
        public async Task SearchMovies_WithEmptyQuery_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.SearchMovies("");

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task SearchMovies_WithValidQuery_ReturnsOkResult()
        {
            // Arrange
            var mockMovies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Bird Box" }
            };
            _mockRepository.Setup(repo => repo.SearchMoviesAsync("birdbox"))
                .ReturnsAsync(mockMovies);

            // Act
            var result = await _controller.SearchMovies("birdbox");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var movies = Assert.IsType<List<Movie>>(okResult.Value);
            Assert.Single(movies);
        }

        [Fact]
        public async Task GetMovieById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var mockMovie = new MovieDetails { Id = 550, Title = "Fight Club" };
            _mockRepository.Setup(repo => repo.GetMovieByIdAsync(550))
                .ReturnsAsync(mockMovie);

            // Act
            var result = await _controller.GetMovieById(550);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var movie = Assert.IsType<MovieDetails>(okResult.Value);
            Assert.Equal(550, movie.Id);
        }
    }
}