using LibraryManagement.Data.Entities;
using LibraryManagement.Repository.Contracts;
using LibraryManagement.Services.Contracts;
using LibraryManagement.ViewModels.Users;

namespace LibraryManagement.Services
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly IRepositoryBase<User, long> usersRepository;

        public AccountManagementService(
            IRepositoryBase<User, long> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<GetUserVM> AuthenticateAsync(string userName, string password)
        {
            var usersByEmail = await usersRepository.GetAsync(x => x.Username.Equals(userName));

            var user = usersByEmail.FirstOrDefault();

            if (user is null)
            {
                return null;
            }

            if (!user.Password.Equals(password))
            {
                return null;
            }

            return new GetUserVM
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Role = user.Role
            };
        }
    }
}
