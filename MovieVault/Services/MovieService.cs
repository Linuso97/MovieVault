using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MovieVault.Data;
using MovieVault.Models.Movies;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MovieVault.Services;

public class MovieService
{
    private readonly ApplicationDbContext _context;
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    public MovieService(ApplicationDbContext context, HttpClient httpClient, IMapper mapper)
    {
        _context = context;
        _httpClient = httpClient;
        _mapper = mapper;
    }
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
}
