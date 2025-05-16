using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Data.Entities
{
    public class BookAuthor : BaseEntity<long>
    {
        public long BookId { get; set; }

        [ForeignKey(nameof(BookId))]

        public virtual Book Book { get; set; } = default!;
        public long AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]

        public virtual Author Author { get; set; } = default!;
    }
}
