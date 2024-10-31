using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieVault.Models;
using MovieVault.Models.Movies;
using MovieVault.Services;
using System.Diagnostics;
using System.Security.Claims;

namespace MovieVault.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        private const string TitleExistsValidationMessage = "This movie already exists in your list";

        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Get(string title)
        {
            var movie = await _movieService.GetMovieAsync(title);

            if(movie == null)
            {
                return NotFound();
            }
            return Json(movie);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save([FromBody] MovieDescriptionVM movieDescription)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return BadRequest(new { message = "User not logged in." });
            }

            if (await _movieService.CheckIfMovieExistsAsync(movieDescription.Title, userId))
            {
                ModelState.AddModelError(nameof(movieDescription.Title), TitleExistsValidationMessage);
            }

            if(ModelState.IsValid)
            {
                await _movieService.SaveMovieAsync(movieDescription, userId);
                return Ok(new { message = "Movie saved successfully!" });
            }
            return BadRequest(new { message = "Model state is invalid." });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
