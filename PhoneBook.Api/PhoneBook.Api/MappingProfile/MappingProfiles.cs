using AutoMapper;
using PhoneBook.Database.Model;
using PhoneBook.Dto;

namespace PhoneBook.MappingProfile
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() { 
            CreateMap<Contact,ContactDto>().ReverseMap();
            CreateMap<AddContactRequestDto, Contact>().ReverseMap();

        }
    }
}
