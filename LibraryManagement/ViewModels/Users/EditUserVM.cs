using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels.Users
{
    public class EditUserVM
    {
        public long Id { get; set; }

        [DisplayName("Username ")]

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = default!;

        [DisplayName("First Name: ")]

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = default!;

        [DisplayName("Last Name: ")]

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = default!;

        [DisplayName("Password: ")]

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = default!;

    }
}
