using LibraryManagement.ViewModels.Users;

namespace LibraryManagement.Services.Contracts
{
    public interface IUserManagementService
    {
        Task<GetUsersVM> GetAllUsersAsync();
        Task<GetUserVM> GetUserByIdAsync(long id);
        Task AddUserAsync(CreateUserVM user);
        Task UpdateUserAsync(EditUserVM user);
        Task DeleteUserAsync(long userId);
    }
}
