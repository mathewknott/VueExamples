using ApiCore.DTO.Acme;
using Microsoft.EntityFrameworkCore;

namespace ApiCore.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Activity>()
                .HasIndex(u => u.Code)
                .IsUnique();

            builder.Entity<Registration>()
                .HasIndex(u => u.RegistrationNumber)
                .IsUnique();


            builder.Entity<User>()
                .HasIndex(u => u.EmailAddress)
                .IsUnique();
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Activity> Activity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Registration> Registration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<User> User { get; set; }
    }
}
