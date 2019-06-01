using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DDDiplom.Models
{
    public class DDDiplomContext : DbContext
    {
        public DDDiplomContext (DbContextOptions<DDDiplomContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<WorkPlace> WorkPlace { get; set; }
        public DbSet<Client> Client { get; set; }

        public DbSet<Role> Roles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = DESKTOP-OJRCM5Q\\SQLEXPRESS; Database = DDDiplomContext; Trusted_Connection = True;");
        }
    }
}


