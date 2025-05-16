using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels.Books
{
    public class CreateBookVM
    {
        [DisplayName("Title: ")]

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = default!;

        [DisplayName("Genre: ")]

        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; } = default!;
        //для того чтобы хранить только уникальные значения
        [DisplayName("Authors: ")]
        [Required(ErrorMessage = "Authors are required")]
        public HashSet<long> AuthorIds { get; set; } = default!;
    }
}
