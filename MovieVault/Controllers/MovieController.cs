using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieVault.Data;
using MovieVault.Models.Movies;
using MovieVault.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MovieVault.Controllers
{
    [Authorize]
    public class MovieController(IMovieService _movieService) : Controller
    {
        // GET: Movie
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return BadRequest("You need to be logged in to save to your list.");
            }
            var viewData = await _movieService.GetAllMoviesAsync(userId);
            return View(viewData);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetMovieFromListAsync<MovieDescriptionVM>(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetMovieFromListAsync<MoviesReadOnlyVM>(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _movieService.DeleteMovieAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
