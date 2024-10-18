namespace MovieVault.Models.Movies

{
    public abstract class BaseMoviesVM
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public string Runtime { get; set; }
        public string imdbRating { get; set; }
    }
}
