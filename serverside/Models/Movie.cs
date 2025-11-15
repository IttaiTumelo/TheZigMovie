using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace serverside.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }
        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }
        public double Popularity { get; set; }
        [JsonPropertyName("vote_average")]
        
        public double VoteAverage { get; set; }
        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }
        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }
        public bool Adult { get; set; }
        [JsonPropertyName("genre_ids")]
        public List<int> GenreIds { get; set; }
        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }
        [JsonPropertyName("original_title")]
        public string OriginalTitle { get; set; }
    }
}