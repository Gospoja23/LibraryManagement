using LibraryManagement.ViewModels.BorrowedBooks;

namespace LibraryManagement.Services.Contracts
{
    public interface ILoanManagementService
    {
        Task<GetLoansVM> GetUserLoans(long userId);

        Task BorrowBook(long bookId, long userId);

        Task ReturnBook(long loanId, long userId);
    }
}
