using LibraryManagement.ViewModels.Authors;

namespace LibraryManagement.Services.Contracts
{
    public interface IAuthorManagementService
    {
        Task<GetAuthorsVM> GetAllAuthorsAsync();
        Task<GetAuthorVM> GetAuthorByIdAsync(long id);
        Task AddAuthorAsync(CreateAuthorVM author);
        Task UpdateAuthorAsync(EditAuthorVM author);
        Task DeleteAuthorAsync(long authorId);
    }
}
