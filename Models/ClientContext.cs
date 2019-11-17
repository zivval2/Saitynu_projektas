using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynu_projektas.Models
{
    public class ClientContext :DbContext
    {
        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Registration> Registrations { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Time> Times { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelbuilder);
        }
    }
}
