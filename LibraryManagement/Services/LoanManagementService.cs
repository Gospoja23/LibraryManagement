using LibraryManagement.Data.Entities;
using LibraryManagement.Repository.Contracts;
using LibraryManagement.Services.Contracts;
using LibraryManagement.ViewModels.BorrowedBooks;

namespace LibraryManagement.Services
{
    public class LoanManagementService : ILoanManagementService
    {
        private readonly IRepositoryBase<Loan, long> loansRepository;
        private readonly IRepositoryBase<Book, long> booksRepository;

        public LoanManagementService(IRepositoryBase<Loan, long> loansRepository, IRepositoryBase<Book, long> booksRepository)
        {
            this.loansRepository = loansRepository;
            this.booksRepository = booksRepository;
        }


        public async Task<GetLoansVM> GetUserLoans(long userId)
        {
            var userActiveLoans = await loansRepository.GetAsync(l => l.UserId == userId && l.ReturnDate == null);

            var getLoansVM = new GetLoansVM
            {
                Items = userActiveLoans.Select(loan => new GetLoanVM
                {
                    Id = loan.Id,
                    BookId = loan.BookId,
                    UserId = loan.UserId,
                    BorrowDate = loan.BorrowDate,
                    ReturnDate = loan.ReturnDate
                }).ToList()
            };

            return getLoansVM;
        }

        public async Task BorrowBook(long bookId, long userId)
        {
            var book = await booksRepository.GetByIdAsync(bookId);

            var loan = new Loan
            {
                BookId = bookId,
                UserId = userId,
                BorrowDate = DateTime.UtcNow,
            };

            book.IsTaken = true;

            await loansRepository.AddAsync(loan);
            await booksRepository.UpdateAsync(book);
        }

        public async Task ReturnBook(long loanId, long userId)
        {
            var loan = await loansRepository.GetByIdAsync(loanId);

            if (loan.User.Id != userId)
            {
                throw new Exception("Wrong user id");
            }

            var book = await booksRepository.GetByIdAsync(loan.BookId);

            loan.ReturnDate = DateTime.UtcNow;

            book.IsTaken = false;

            await loansRepository.UpdateAsync(loan);
            await booksRepository.UpdateAsync(book);
        }
    }
}
