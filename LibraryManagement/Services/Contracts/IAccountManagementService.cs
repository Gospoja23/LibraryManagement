using LibraryManagement.ViewModels.Users;

namespace LibraryManagement.Services.Contracts
{
    public interface IAccountManagementService
    {
        Task<GetUserVM> AuthenticateAsync(string userName, string password);
    }
}
