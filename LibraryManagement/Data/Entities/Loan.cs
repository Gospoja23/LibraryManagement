using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Data.Entities
{
    public class Loan : BaseEntity<long>
    {
        public long UserId { get; set; }
        public long BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = default!;

        [ForeignKey(nameof(BookId))]
        public virtual Book Book { get; set; } = default!;
    }
}
