using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using prismtest2.Models.Entity;

namespace prismtest2.Models.Context
{
    public class DataBaseContext:DbContext
    {
        public DataBaseContext()
        {
            
        }
        public DbSet<Users> username { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.; Database=SampleProject; Integrated Security=True; Encrypt=False");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasKey(e => e.Id);
        }
    }
}
