using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Adoptly.Models;

namespace Adoptly.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Adoptly.Models.Post> Post { get; set; }
        public DbSet<Adoptly.Models.Udruga> Udruga { get; set; }
        public DbSet<Adoptly.Models.Ljubimac> Ljubimac { get; set; }
        public DbSet<Adoptly.Models.Skloniste> Skloniste { get; set; }
        public DbSet<Adoptly.Models.Posvajatelj> Posvajatelj { get; set; }
    }
}
