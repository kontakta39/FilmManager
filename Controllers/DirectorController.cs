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
    public class DirectorController : Controller
    {
        private readonly UserManager<FilmManagerUser> userManager;
        private readonly ApplicationDbContext context;

        public DirectorController(UserManager<FilmManagerUser> userManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public ActionResult Search(string SearchBox)
        {
            var directors = (from t in context.Directors
                             where
                                 t.FirstName.Contains(SearchBox)
                                 || t.LastName.Contains(SearchBox)
                             select t).ToList();
            return View("Directors", directors);
        }

        public async Task<ActionResult> Directors()
        {
            List<Director> directors = await context.Directors.ToListAsync();

            return View(directors);
        }

        public async Task<IActionResult> Details(int? directorId)
        {
            if (directorId == null)
            {
                return NotFound();
            }

            var director = await context.Directors
                .FirstOrDefaultAsync(m => m.DirectorId == directorId);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            Director director = new Director()
            {
                Films = await context.Films.ToListAsync()
            };

            return View(director);

        }

        [HttpPost]
        public async Task<ActionResult> Create(Director director)
        {
            if (!ModelState.IsValid)
            {
                return View(director);
            }

            await context.Directors.AddAsync(director);
            await context.SaveChangesAsync();

            return RedirectToAction("Directors");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int directorId)
        {
            Director director = await context.Directors.FindAsync(directorId);
            director.Films = await context.Films.ToListAsync();

            return View(director);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Director director, int directorId)
        {
            if (!ModelState.IsValid)
            {
                return View(director);
            }

            director.DirectorId = directorId;

            context.Directors.Update(director);
            await context.SaveChangesAsync();

            return RedirectToAction("Directors");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int directorId)
        {
            Director director = await context.Directors.FindAsync(directorId);
            return View(director);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int directorId)
        {
            Director director = await context.Directors.FindAsync(directorId);
            context.Directors.Remove(director);
            await context.SaveChangesAsync();
            return RedirectToAction("Directors");
        }
    }
}
