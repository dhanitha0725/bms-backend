using bms.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace bms.Infrastructure.DbContexts
{
    public class BookDbContext : DbContext
    {
        // Define DbSets for your entities
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configurations
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.Username).IsUnique();
                entity.Property(u => u.Username)
                    
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(u => u.PasswordHash)
                    .IsRequired();
            });

            // Book configurations
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);
                
                entity.Property(b => b.Title)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(b => b.Author)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(b => b.Genre)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(b => b.PublishedYear)
                    .IsRequired();

                // one-to-many relationship with User
                entity.HasOne(b => b.User)
                    .WithMany(u => u.Books)
                    .HasForeignKey(b => b.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
