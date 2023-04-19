using Microsoft.EntityFrameworkCore;
using VetClinicServer.Model.Models;

namespace VetClinicServer.Model.Context
{
    public class VetClinicContext : DbContext
    {
        public VetClinicContext(DbContextOptions<VetClinicContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public DbSet<Animal> Animals { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}
