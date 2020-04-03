using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp12.Models;

namespace WpfApp12.Helpers
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ApplicationContext>(null);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Guests> Guests { get; set; }
        public DbSet<Hotels> Hotels { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
        
    }
}
