using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication_Practice.Models;

namespace WebApplication_Practice.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebApplication_Practice.Models.Album> Album { get; set; }
        public DbSet<WebApplication_Practice.Models.Artist> Artist { get; set; }
        public DbSet<WebApplication_Practice.Models.Genre> Genre { get; set; }
        public DbSet<WebApplication_Practice.Models.Song> Song { get; set; }
    }
}
