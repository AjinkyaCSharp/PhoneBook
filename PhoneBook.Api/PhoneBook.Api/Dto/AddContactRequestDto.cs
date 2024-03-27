namespace PhoneBook.Dto
{
    public class AddContactRequestDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PrimaryContact { get; set; }
        public string? SecondaryContact { get; set; }
        public string? Address { get; set; }
    }
}
