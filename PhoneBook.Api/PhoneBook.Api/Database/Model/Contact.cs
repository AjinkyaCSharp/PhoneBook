namespace PhoneBook.Database.Model
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PrimaryContact { get; set; }
        public string? SecondaryContact { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
