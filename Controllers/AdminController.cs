using FilmManager.Data;
using FilmManager.Models.Account;
using FilmManager.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<FilmManagerUser> userManager;
        private readonly ApplicationDbContext context;

        public AdminController(UserManager<FilmManagerUser> userManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<IActionResult> GuestsList()
        {
            var users = (await userManager.GetUsersInRoleAsync("Guest")).ToList();
            return View(users);
        }

        public async Task<IActionResult> MakeAdmin(string username)
        {
            // 1. Find the user with this username
            var user = await userManager.FindByNameAsync(username);

            // 2. Add the Admin role to this user
            var result = await userManager.AddToRoleAsync(user, "Admin");

            // 3. Return the proper view
            if (result.Succeeded)
            {
                await userManager.RemoveFromRoleAsync(user, "Guest");
                var users = (await userManager.GetUsersInRoleAsync("Guest")).ToList();
                return View("GuestsList", users);
            }

            // 4. Return if could not remove from role
            return View("GuestsList", new List<FilmManagerUser>());
        }
    }
}