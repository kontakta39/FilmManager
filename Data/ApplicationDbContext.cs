﻿using System;
using System.Collections.Generic;
using System.Text;
using FilmManager.Models.Account;
using FilmManager.Models.Genre;
using FilmManager.Models.Film;
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

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Film> Films { get; set; }

    }
}