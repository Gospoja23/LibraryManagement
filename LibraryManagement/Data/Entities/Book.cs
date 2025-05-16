namespace LibraryManagement.Data.Entities
{
    public class Book : BaseEntity<long>
    {
        public string Title { get; set; } = default!;
        public string Genre { get; set; } = default!;
        public bool IsTaken { get; set; } = default!;

        public virtual ICollection<Loan> Loans { get; set; } = default!;

        public virtual ICollection<Author> Authors { get; set; } = default!;
        public virtual ICollection<BookAuthor> BookAuthors { get; set; } = default!;
    }
}
