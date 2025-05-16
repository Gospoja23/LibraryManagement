using LibraryManagement.Data.Entities;
using LibraryManagement.Repository.Contracts;
using LibraryManagement.Services.Contracts;
using LibraryManagement.ViewModels.Users;

namespace LibraryManagement.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IRepositoryBase<User, long> usersRepository;

        public UserManagementService(
            IRepositoryBase<User, long> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<GetUsersVM> GetAllUsersAsync()
        {
            var users = await usersRepository.GetAllAsync();

            var getUsersVM = new GetUsersVM()
            {
                Items = users.Select(user => new GetUserVM
                {
                    Id = user.Id,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    Role = user.Role
                }).ToList()
            };

            return getUsersVM;
        }

        public async Task<GetUserVM> GetUserByIdAsync(long id)
        {
            var user = await usersRepository.GetByIdAsync(id);

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

        public async Task AddUserAsync(CreateUserVM user)
        {
            var userToAdd = new User
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Role = UserRole.User
            };

            await usersRepository.AddAsync(userToAdd);
        }

        public async Task UpdateUserAsync(EditUserVM user)
        {
            var userToUpdate = new User
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
            };

            await usersRepository.UpdateAsync(userToUpdate);
        }

        public async Task DeleteUserAsync(long userId)
        {
            var userToDelete = await usersRepository.GetByIdAsync(userId);

            await usersRepository.DeleteAsync(userToDelete);
        }
    }
}
