using LibraryManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public class LibraryManagementDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=localhost;Database=LibraryManagementDB;Trusted_Connection=True;TrustServerCertificate=True;")
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // этот метод с помощью рефлексии находит все конфигурации для наших Entity и применяет их
            //конфигурации находятся в папке Data/Configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryManagementDbContext).Assembly);

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Username = "valeri",
                    Password = "4444",
                    FirstName = "Valeria ",
                    LastName = "Degt ",
                    Role = UserRole.Admin
                });
            //этот метод вызывает базовую реализацию метода OnModelCreating
            base.OnModelCreating(modelBuilder);

        }

    }
}
