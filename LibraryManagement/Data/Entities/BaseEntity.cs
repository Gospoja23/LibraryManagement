using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Data.Entities
{
    public abstract class BaseEntity<TType>
    {
        [Key]
        [Required]

        public TType Id { get; set; } = default!;
    }
}
