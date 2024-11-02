
namespace MovieVault.Controllers
{
    public class HomeController(IMovieService _movieService) : Controller
    {
        private const string TitleExistsValidationMessage = "This movie already exists in your list";

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
                return BadRequest(new { message = "This object already exists in your list." });
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
