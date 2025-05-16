namespace LibraryManagement.Data.Entities
{
    public class User : BaseEntity<long>
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public UserRole Role { get; set; } = default!;

        public virtual ICollection<Loan> Loans { get; set; } = default!;

    }

    public enum UserRole
    {
        Admin,
        User
    }
}
