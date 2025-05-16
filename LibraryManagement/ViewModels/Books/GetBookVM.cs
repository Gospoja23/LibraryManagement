namespace LibraryManagement.ViewModels.Books
{
    public class GetBookVM
    {
        public long Id { get; set; }
        public string Title { get; set; } = default!;
        public string Genre { get; set; } = default!;
        public List<GetBookAuthorVM> Authors { get; set; } = default!;
        public bool IsTaken { get; set; } = default!;
    }

    public class GetBookAuthorVM
    {
        public long AuthorId { get; set; }

        public string AuthorFullName { get; set; } = default!;
    }

    public class GetBookLoanDataVM : GetBookVM
    {
        public bool IsTakenByCurrentUser { get; set; }

        public long LoanId { get; set; }

        public long BorrowerId { get; set; }
    }
}
