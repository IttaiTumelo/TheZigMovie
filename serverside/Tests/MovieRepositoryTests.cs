using System.Net;
using Moq;
using Moq.Protected;
using Xunit;

namespace serverside.Tests
{
    public class MovieRepositoryTests
    {
        [Fact]
        public async Task GetPopularMoviesAsync_ReturnsListOfMovies()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(@"{
                        ""results"": [
                            {""id"": 1, ""title"": ""Movie 1""},
                            {""id"": 2, ""title"": ""Movie 2""}
                        ]
                    }")
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c["TheMovieDb:ApiKey"]).Returns("test-key");
            mockConfig.Setup(c => c["TheMovieDb:BaseUrl"]).Returns("https://api.test.com");

            var repository = new MovieRepository(httpClient, mockConfig.Object);

            // Act
            var result = await repository.GetPopularMoviesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}