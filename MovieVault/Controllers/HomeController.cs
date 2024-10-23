using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieVault.Data;
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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save([FromBody] MovieDescriptionVM movieDescription)
        {
            if (await _movieService.CheckIfMovieExists(movieDescription.Title))
            {
                ModelState.AddModelError(nameof(movieDescription.Title), TitleExistsValidationMessage);
            }

            if(ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return BadRequest("You need to be logged in to save to your list.");
                }

                await _movieService.SaveMovieAsync(movieDescription, userId);
                return RedirectToAction(nameof(Index));
            }
            return View(movieDescription);
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
