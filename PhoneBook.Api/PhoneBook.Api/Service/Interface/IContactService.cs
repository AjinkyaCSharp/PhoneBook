using PhoneBook.Dto;

namespace PhoneBook.Service
{
    public interface IContactService
    {
        public Task<Guid?> AddContact(AddContactRequestDto contactDto);
        public Task<bool> DeleteContactById(Guid contactId);
        public Task<ContactDto> GetContactById(Guid contactId);
        public Task<IEnumerable<ContactDto>> GetContacts(int offset,int numRecords);
        public Task<ContactDto> UpdateContact(ContactDto contact);
    }
}
