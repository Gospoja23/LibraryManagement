using LibraryManagement.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;

namespace LibraryManagement.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {

        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(500);

            builder.Property(x => x.Genre)
            .HasMaxLength(500);

            builder
      .HasMany(e => e.Authors)
      .WithMany(e => e.Books)
      .UsingEntity<BookAuthor>();

        }
    }
}
