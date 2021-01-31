using FilmManager.Data;
using FilmManager.Models.Account;
using FilmManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmManager.Controllers
{
    public class FilmController : Controller
    {
        private readonly UserManager<FilmManagerUser> userManager;
        private readonly ApplicationDbContext context;

        public FilmController(UserManager<FilmManagerUser> userManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public ActionResult Search(string SearchBox)
        {
            var films = (from t in context.Films
                         where
                             t.Title.Contains(SearchBox)
                         select t).ToList();
            return View("Films", films);
        }

        public async Task<ActionResult> Films()
        {
            List<Film> films = await context.Films.ToListAsync();

            return View(films);
        }

        public async Task<IActionResult> Details(int? filmId)
        {
            if (filmId == null)
            {
                return NotFound();
            }

            var film = await context.Films
                .FirstOrDefaultAsync(m => m.FilmId == filmId);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            Film film = new Film()
            {
                Genres = await context.Genres.ToListAsync()
            };

            return View(film);

        }

        [HttpPost]
        public async Task<ActionResult> Create(Film film)
        {
            if (!ModelState.IsValid)
            {
                return View(film);
            }

            await context.Films.AddAsync(film);
            await context.SaveChangesAsync();

            return RedirectToAction("Films");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int filmId)
        {
            Film film = await context.Films.FindAsync(filmId);
            film.Genres = await context.Genres.ToListAsync();

            return View(film);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Film film, int filmId)
        {
            if (!ModelState.IsValid)
            {
                return View(film);
            }

            film.FilmId = filmId;

            context.Films.Update(film);
            await context.SaveChangesAsync();

            return RedirectToAction("Films");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int filmId)
        {
            Film film = await context.Films.FindAsync(filmId);
            return View(film);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int filmId)
        {
            Film film = await context.Films.FindAsync(filmId);
            context.Films.Remove(film);
            await context.SaveChangesAsync();
            return RedirectToAction("Films");
        }
    }
}
