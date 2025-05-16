using LibraryManagement.Data.Entities;

namespace LibraryManagement.ViewModels.Users
{
    public class GetUserVM
    {
        public long Id { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public UserRole Role { get; set; } = default!;
    }
}
