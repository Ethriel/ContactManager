using ContactManagerDB.Utility;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerDB.Model
{
    public class ContactDbContext : DbContext
    {
        public virtual DbSet<Contact> Contacts { get; set; }

        public ContactDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionStrings.DefaultConnection);
            }
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>()
                        .HasKey(x => x.Id);

            modelBuilder.Entity<Contact>()
                        .HasIndex(x => x.Phone)
                        .IsUnique();

            modelBuilder.Entity<Contact>()
                        .Property(x => x.Salary)
                        .HasPrecision(18, 2);
        }
    }
}
