namespace serverside.Models
{
    public class MovieDetails : Movie
    {
        public string Homepage { get; set; }
        public int Budget { get; set; }
        public long Revenue { get; set; }
        public int Runtime { get; set; }
        public string Status { get; set; }
        public string Tagline { get; set; }
        public List<Genre> Genres { get; set; }
        public List<ProductionCompany> ProductionCompanies { get; set; }
    }
}