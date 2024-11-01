namespace MovieVault.Application.Services;

public class MovieService(ApplicationDbContext _context, HttpClient _httpClient, IMapper _mapper) : IMovieService
{
    public async Task<MovieDescriptionVM?> GetMovieAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return null;
        }

        var response = await _httpClient.GetStringAsync
            ($"http://www.omdbapi.com/?t={Uri.EscapeDataString(title)}&apikey=c74d46b0");
        var data = JsonSerializer.Deserialize<MovieApiResponse>(response);

        if (data == null || data.Response == "False")
        {
            return null;
        }

        var viewData = _mapper.Map<MovieDescriptionVM>(data);
        return viewData;
    }
    public async Task<T?> GetMovieFromListAsync<T>(int id) where T : class
    {
        var data = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
        if (data == null)
        {
            return null;
        }

        var viewData = _mapper.Map<T>(data);
        return viewData;
    }

    public async Task<List<MoviesReadOnlyVM>> GetAllMoviesAsync(string userId)
    {

        var data = await _context.Movies
            .Where(q => q.UserId == userId)
            .ToListAsync();
        var viewData = _mapper.Map<List<MoviesReadOnlyVM>>(data);
        return viewData;
    }

    public async Task DeleteMovieAsync(int id)
    {
        var data = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
        if (data != null)
        {
            _context.Movies.Remove(data);
            _context.SaveChanges();
        }
    }

    public async Task SaveMovieAsync(MovieDescriptionVM data, string userId)
    {
        var movie = _mapper.Map<Movie>(data);
        movie.UserId = userId;
        _context.Add(movie);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CheckIfMovieExistsAsync(string name, string userId)
    {
        var lowercaseName = name.ToLower();
        return await _context.Movies.AnyAsync(q => q.Title
            .ToLower()
            .Equals(lowercaseName)
            && q.UserId == userId);

    }
}
