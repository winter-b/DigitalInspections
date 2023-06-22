using DigitalInspectionsWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System.Reflection.Metadata;

namespace DigitalInspectionsWebApi.Helpers
{
    public class DigitalInspectionsDbContext : DbContext
    {
        public DigitalInspectionsDbContext(DbContextOptions<DigitalInspectionsDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Work> Works{ get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Machine> Machines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=database;Port=5432;Database=DigitalInspections;Username=user;Password=password;");
    }
}

