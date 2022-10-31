using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductsAndCategories.Models;

namespace ProductsAndCategories
{
    public class myDBContext : DbContext
    {
        public DbSet<Products> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public myDBContext(DbContextOptions<myDBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ProductsDB;");
        }
    }
}

