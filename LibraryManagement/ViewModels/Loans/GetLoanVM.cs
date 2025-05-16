namespace LibraryManagement.ViewModels.BorrowedBooks
{
    public class GetLoanVM
    {
        public long Id { get; set; }

        public long BookId { get; set; }

        public long UserId { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
