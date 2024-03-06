using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryConsoleApp.Model
{
    public class AppDbContext : DbContext
    {
        public DbSet<Draw> Draws { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=LotteryAppDb;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True");
            //optionsBuilder.UseSqlServer("");

        }
    }
}
