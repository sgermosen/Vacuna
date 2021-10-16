using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VacunaAPI.Entities;

namespace VacunaAPI
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<MoviesActors>()
            //    .HasKey(x => new { x.ActorId, x.MovieId });
           
            //modelBuilder.Entity<MoviesTheaters>()
            // .HasKey(x => new { x.TheaterId, x.MovieId });
          
            //modelBuilder.Entity<MoviesGenders>()
            //             .HasKey(x => new { x.GenderId, x.MovieId });

            base.OnModelCreating(modelBuilder);
        }
         
        public DbSet<Immunization> Immunizations { get; set; }
        public DbSet<VacunationCenter> VacunationCenters { get; set; }
        public DbSet<Laboratory> Laboratories { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }

        public DbSet<Image> Images { get; set; }
        public DbSet<ImageType> ImageTypes { get; set; }


    }
}
