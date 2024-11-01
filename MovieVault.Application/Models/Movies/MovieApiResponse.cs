namespace MovieVault.Application.Models.Movies
{
    public class MovieApiResponse : BaseMoviesVM
    {
        public string Response { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Poster { get; set; }
    }
}
