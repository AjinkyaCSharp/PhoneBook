using AutoMapper;
using PhoneBook.Database;
using PhoneBook.Database.Model;
using PhoneBook.Dto;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace PhoneBook.Repository
{
    [ExcludeFromCodeCoverage]
    public class ContactRepository : IConatactRepository
    {
        private readonly PhoneBookDbContext phoneBookDbContext;
        private readonly IMapper mapper;
        public ContactRepository(PhoneBookDbContext phoneBookDbContext, IMapper mapper)
        {
            this.phoneBookDbContext = phoneBookDbContext;
            this.mapper = mapper;
        }

        public async Task<Guid?> Add(AddContactRequestDto contactRequestDto)
        {
            var contact = mapper.Map<Contact>(contactRequestDto);
            contact.Id = Guid.NewGuid();
            phoneBookDbContext.Add(contact);
            await phoneBookDbContext.SaveChangesAsync();
            return contact.Id;
        }

        public async Task DeleteById(Guid Id)
        {
            var contact = phoneBookDbContext.Contacts.Find(Id);
            if (contact != null)
            {
                phoneBookDbContext.Contacts.Remove(contact);
                await phoneBookDbContext.SaveChangesAsync();
            }
            else
                throw new Exception();
        }

        public async Task<IEnumerable<ContactDto>> GetAll(int offset, int numberOfRecords)
        {
            var contacts= phoneBookDbContext.Contacts.OrderByDescending(x=>x.CreatedTime).Skip(offset).Take(numberOfRecords);
            return mapper.Map<IEnumerable<ContactDto>>(contacts);
        }

        public async Task<ContactDto> GetById(Guid Id)
        {
            var contact= await phoneBookDbContext.Contacts.FindAsync(Id);
            return mapper.Map<ContactDto>(contact);
        }

        public async Task<ContactDto> Update(ContactDto contactDto)
        {
            var contact=phoneBookDbContext.Contacts.Find(contactDto.Id);
            if(contact != null)
            {
                contact.Address = contactDto.Address;
                contact.Email= contactDto.Email;
                contact.PrimaryContact= contactDto.PrimaryContact;
                contact.SecondaryContact=contactDto.SecondaryContact;
                contactDto.FirstName= contactDto.FirstName;
                contactDto.LastName= contactDto.LastName;
                await phoneBookDbContext.SaveChangesAsync();
                return contactDto;
            }
            else throw new Exception();
        }
    }
}
