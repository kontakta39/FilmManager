using FilmManager.Data;
using FilmManager.Models.Account;
using FilmManager.Models.Genre;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmManager.Controllers
{
    public class GenreController : Controller
    {
        private readonly UserManager<FilmManagerUser> userManager;
        private readonly ApplicationDbContext context;

        public GenreController(UserManager<FilmManagerUser> userManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<ActionResult> Genres()
        {
            // 1. Take all genres as list
            List<Genre> genres = await context.Genres.ToListAsync();

            // 2. Return the view
            return View(genres);
        }

        // GET: Genre/Details/5
        public async Task<IActionResult> Details(int? genreId)
        {
            if (genreId == null)
            {
                return NotFound();
            }

            var genre = await context.Genres
                .FirstOrDefaultAsync(m => m.GenreId == genreId);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View(genre);
            }

            await context.Genres.AddAsync(genre);
            await context.SaveChangesAsync();

            return RedirectToAction("Genres");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int genreId)
        {
            Genre genre = await context.Genres.FindAsync(genreId);
            return View(genre);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Genre genre, int genreId)
        {
            if (!ModelState.IsValid)
            {
                return View(genre);
            }

            genre.GenreId = genreId;

            context.Genres.Update(genre);
            await context.SaveChangesAsync();

            return RedirectToAction("Genres");
        }

        //public async Task<ActionResult> Delete(int genreId)
        //{
        //    Genre genre = await context.Genres.FindAsync(genreId);

        //    context.Genres.Remove(genre);
        //    await context.SaveChangesAsync();

        //    return RedirectToAction("Genres");
        //}

        [HttpGet]
        public async Task<ActionResult> Delete(int genreId)
        {
            Genre genre = await context.Genres.FindAsync(genreId);
            return View(genre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int genreId)
        {
            Genre genre = await context.Genres.FindAsync(genreId);
            context.Genres.Remove(genre);
            await context.SaveChangesAsync();
            return RedirectToAction("Genres");
        }

        //// GET: Car/Delete/5
        //public async Task<IActionResult> Delete(int? genreId)
        //{
        //    if (genreId == null)
        //    {
        //        return NotFound();
        //    }

        //    var genre = await context.Genres
        //        .FirstOrDefaultAsync(m => m.GenreId == genreId);
        //    if (genre == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(genre);
        //}

        //// POST: Car/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int genreId)
        //{
        //    var genre = await context.Genres.FindAsync(genreId);
        //    context.Genres.Remove(genre);
        //    await context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Genres));
        //}

        //private bool GenreExists(int genreId)
        //{
        //    return context.Genres.Any(e => e.GenreId == genreId);
        //}
    }
}

