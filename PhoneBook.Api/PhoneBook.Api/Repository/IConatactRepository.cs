using PhoneBook.Dto;

namespace PhoneBook.Repository
{
    public interface IConatactRepository
    {
        public Task<Guid?> Add(AddContactRequestDto contactRequestDto);
        public Task<ContactDto> GetById(Guid Id);
        public Task DeleteById(Guid Id);
        public Task<IEnumerable<ContactDto>> GetAll(int offset, int numberOfRecords);
        public Task<ContactDto> Update(ContactDto contactDto);
    }
}
