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

namespace MovieVault.Controllers
{
    public class MovieController(IMovieService _movieService) : Controller
    {
        // GET: Movie
        public async Task<IActionResult> Index()
        {
            var viewData = await _movieService.GetAllMoviesAsync();
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
