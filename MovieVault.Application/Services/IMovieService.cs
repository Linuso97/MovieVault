﻿namespace MovieVault.Application.Services
{
    public interface IMovieService
    {
        Task<bool> CheckIfMovieExistsAsync(string name, string userId);
        Task DeleteMovieAsync(int id);
        Task<List<MoviesReadOnlyVM>> GetAllMoviesAsync(string userId);
        Task<MovieDescriptionVM?> GetMovieAsync(string title);
        Task<T?> GetMovieFromListAsync<T>(int id) where T : class;
        Task SaveMovieAsync(MovieDescriptionVM data, string userId);
    }
}