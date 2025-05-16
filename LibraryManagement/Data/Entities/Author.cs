using System.Collections.Generic;

namespace LibraryManagement.Data.Entities
{
    public class Author : BaseEntity<long>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;

        public virtual ICollection<Book> Books { get; set; } = default!;
        public virtual ICollection<BookAuthor> BookAuthors { get; set; } = default!;


    }
}
