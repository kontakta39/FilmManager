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
                              || t.Genre.Contains(SearchBox)
                              || t.Director.Contains(SearchBox)
                              || t.Music.Contains(SearchBox)
                              || t.Distributor.Contains(SearchBox)                            
                         select t).ToList();
            return View("Films", films);
        }

        public async Task<ActionResult> Films()
        {
            // 1. Take all films as list
            List<Film> films = await context.Films.ToListAsync();

            // 2. Return the view
            return View(films);
        }

        // GET: Genre/Details/5
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
        public ActionResult Create()
        {
            return View();
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
