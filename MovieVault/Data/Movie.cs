﻿namespace MovieVault.Data
{
    public class Movie
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string imdbRating { get; set; }
        public string Poster { get; set; }
    }
}
