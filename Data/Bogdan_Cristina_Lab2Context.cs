using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bogdan_Cristina_Lab2.Models;

namespace Bogdan_Cristina_Lab2.Data
{
    public class Bogdan_Cristina_Lab2Context : DbContext
    {
        public Bogdan_Cristina_Lab2Context(DbContextOptions<Bogdan_Cristina_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
