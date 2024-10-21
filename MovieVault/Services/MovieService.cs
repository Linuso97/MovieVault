using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MovieVault.Data;
using MovieVault.Models.Movies;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.ComponentModel;

namespace MovieVault.Services;

public class MovieService(ApplicationDbContext _context, HttpClient _httpClient, IMapper _mapper)
{
    public async Task<MovieDescriptionVM?> GetMovieAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return null;
        }
        var response = await _httpClient.GetStringAsync($"http://www.omdbapi.com/?t={title}&apikey=c74d46b0");
        var data = JsonSerializer.Deserialize<Movie>(response);
        var viewData = _mapper.Map<MovieDescriptionVM>(data);
        return viewData;
    }
    public async Task<T?>GetMovieFromListAsync<T>(int id) where T : class
    {
        var data = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
        if (data == null)
        {
            return null;
        }

        var viewData = _mapper.Map<T>(data);
        return (viewData);
    }

    public async Task<List<MoviesReadOnlyVM>> GetAllMoviesAsync()
    {
        var data = await _context.Movies.ToListAsync();
        var viewData = _mapper.Map<List<MoviesReadOnlyVM>>(data);
        return viewData;
    }

    public async Task DeleteMovieAsync(int id)
    {
        var data = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
        if(data != null)
        {
            _context.Movies.Remove(data);
            _context.SaveChanges();
        }
    }

    public async Task SaveMovieAsync(MovieDescriptionVM data)
    {
        var movie = _mapper.Map<MovieDescriptionVM>(data);
        _context.Add(movie);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CheckIfMovieExists(string name)
    {
        var lowercaseName = name.ToLower();
        return await _context.Movies.AnyAsync(q => q.Title.ToLower().Equals(lowercaseName));
    }
}
