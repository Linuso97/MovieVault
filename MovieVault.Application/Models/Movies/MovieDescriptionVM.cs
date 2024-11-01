namespace MovieVault.Application.Models.Movies
{
    public class MovieDescriptionVM : BaseMoviesVM
    {
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Poster { get; set; }
        public string UserId { get; set; }
    }
}
