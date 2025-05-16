using LibraryManagement.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;

namespace LibraryManagement.Data.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        //он определяет структуру таблицы , валидации отдельных полей и связи между таблицами или полями
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .HasMaxLength(500);

            builder.Property(x => x.LastName)
            .HasMaxLength(500);

            builder
      .HasMany(e => e.Books)
      .WithMany(e => e.Authors)
      .UsingEntity<BookAuthor>();

        }
    }
}
