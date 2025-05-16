using LibraryManagement.ViewModels.Books;

namespace LibraryManagement.Services.Contracts
{
    public interface IBookManagementService
    {
        Task<IEnumerable<GetBookVM>> GetAllBooksAsync();
        Task<GetBookVM> GetBookByIdAsync(long id);
        Task AddBookAsync(CreateBookVM book);
        Task UpdateBookAsync(EditBookVM book);
        Task DeleteBookAsync(long bookId);
    }
}
