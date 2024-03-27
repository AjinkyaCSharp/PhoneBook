using AutoMapper;
using PhoneBook.Dto;
using PhoneBook.Repository;

namespace PhoneBook.Service
{
    public class ContactService : IContactService,IDisposable
    {
        //todo add logger
        private readonly IConatactRepository contactRepository;
        private readonly ILogger<ContactService> logger;
        public ContactService(ILogger<ContactService> logger,IConatactRepository phoneRepository) 
        {
           this.contactRepository = phoneRepository;
            this.logger = logger;
        }

        #region service
        public async Task<Guid?> AddContact(AddContactRequestDto contactDto)
        {
            try
            {
                var Id=await contactRepository.Add(contactDto);

                return Id;
            } 
            catch (Exception ex)
            {
                //todo add global exception filter
                logger.LogError(1,exception:ex,message:"Error in adding contact to db");
                return null;
            }
        }

        public async Task<bool> DeleteContactById(Guid contactId)
        {
            try
            {
                if(contactId == Guid.Empty)
                {
                    throw new Exception(message:"Invalid contact id");
                }
                await contactRepository.DeleteById(contactId);
                return true;
            }
            catch(Exception ex)
            {
                logger.LogError(1, exception: ex, message: "Error in deleting contact from db");
                return false;
            }
        }

        public async Task<ContactDto> GetContactById(Guid contactId)
        {
            try
            {
                return await contactRepository.GetById(contactId);
            }
            catch (Exception ex)
            {
                logger.LogError(1, exception: ex, message: "Error in getting contact from db");
                return null;
            }
        }

        public async Task<IEnumerable<ContactDto>> GetContacts(int offset, int numRecords)
        {
            try
            {
                if(offset < 0 || numRecords < 0 || numRecords==0)
                    throw new Exception();
                return await contactRepository.GetAll(offset, numRecords);
            }
            catch (Exception ex)
            {
                logger.LogError(1, exception: ex, message: "Error in getting contacts from db");
                return null;   
            }
        }

        public async Task<ContactDto> UpdateContact(ContactDto contact)
        {
            try
            {
                return await contactRepository.Update(contact);
            }
            catch(Exception ex)
            {
                logger.LogError(1, exception: ex, message: "Error in getting updating contact in db");
                return null;
            }   
        }
        #endregion
        #region dispose
        public void Dispose()
        {
            //this.contactRepository.Dispose();
        }
        #endregion
    }
}
