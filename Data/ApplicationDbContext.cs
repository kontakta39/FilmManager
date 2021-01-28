using System;
using System.Collections.Generic;
using System.Text;
using FilmManager.Models.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FilmManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<FilmManagerUser, FilmManagerRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
