using PhoneBook.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Api.Test.MockData
{
    public class PhoneBookMockData
    {
        public static ContactDto mockContact=new ContactDto
        {
            Address="Test",
            Email="Test",   
            FirstName="Test",   
            LastName="Test",
            Id = Guid.NewGuid(),
            PrimaryContact = "Test",
            SecondaryContact = "Test"   
        };
        public static List<ContactDto> mockContacts = new List<ContactDto>()
        {
            mockContact
        };

        public static AddContactRequestDto mockAddContact = new AddContactRequestDto
        {
            Address = "Test",
            Email = "Test",
            FirstName = "Test",
            LastName = "Test",
            PrimaryContact = "Test",
            SecondaryContact = "Test"
        };
    }
}
