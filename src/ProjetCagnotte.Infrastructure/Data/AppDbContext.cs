using Microsoft.EntityFrameworkCore;
using ProjetCagnotte.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetCagnotte.Infrastructure.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Contribution> Contributions => Set<Contribution>();

    }
}
