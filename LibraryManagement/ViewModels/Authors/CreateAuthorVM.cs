using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels.Authors
{
    public class CreateAuthorVM

    {
        [DisplayName("First Name: ")]

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }=default!;

        [DisplayName("Last Name: ")]

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }= default!;
    }
}
