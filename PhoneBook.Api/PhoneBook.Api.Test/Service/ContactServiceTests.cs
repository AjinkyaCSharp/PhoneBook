using Microsoft.Extensions.Logging;
using Moq;
using PhoneBook.Api.Test.MockData;
using PhoneBook.Dto;
using PhoneBook.Repository;
using PhoneBook.Service;

namespace PhoneBook.Api.Test.Service
{
    public class ContactServiceTests
    {
        private Mock<ILogger<ContactService>> mockLogger;
        private Mock<IConatactRepository> mockContactRepo;
        private ContactService contactService;

        [SetUp] 
        public void SetUp() 
        {
            mockLogger=new Mock<ILogger<ContactService>>();
            mockContactRepo=new Mock<IConatactRepository>();
            contactService = new ContactService(mockLogger.Object,mockContactRepo.Object);
        }

        [Test]
        public void AddContactPositiveTest()
        {
            mockContactRepo.Setup(x => x.Add(It.IsAny<AddContactRequestDto>())).ReturnsAsync(Guid.NewGuid());
            Guid? result = null;
            Assert.DoesNotThrowAsync(async () =>
            {
               result= await contactService.AddContact(PhoneBookMockData.mockAddContact);
            });
            Assert.NotNull(result);
        }

        [Test]
        public void AddContactNegativeTest()
        {
            mockContactRepo.Setup(x => x.Add(It.IsAny<AddContactRequestDto>())).ThrowsAsync(new Exception());
            Guid? result = null;
            Assert.DoesNotThrowAsync(async () =>
            {
                result = await contactService.AddContact(PhoneBookMockData.mockAddContact);
            });
            Assert.IsNull(result);
        }

        [Test]
        public void  DeleteContactByIdPositiveTest()
        {
            mockContactRepo.Setup(x => x.DeleteById(It.IsAny<Guid>())).Returns(Task.CompletedTask);
            bool result = false;
            Assert.DoesNotThrowAsync(async () =>
            {
                result = await contactService.DeleteContactById(Guid.NewGuid());
            });
            Assert.That(result);
        }

        [Test]
        public void DeleteContactByIdNegativeTest()
        {
            mockContactRepo.Setup(x => x.DeleteById(It.IsAny<Guid>())).ThrowsAsync(new Exception());
            bool result = false;
            Assert.DoesNotThrowAsync(async () =>
            {
                result = await contactService.DeleteContactById(Guid.NewGuid());
            });
            Assert.That(!result);
        }

        [Test]
        public void GetContactByIdPositiveTest()
        {
            mockContactRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(PhoneBookMockData.mockContact);
            ContactDto contactDto = null;
            Assert.DoesNotThrowAsync(async () =>
            {
                contactDto =await contactService.GetContactById(Guid.NewGuid());
            });
            Assert.IsNotNull(contactDto);
        }

        [Test]
        public void GetContactByIdNegativeTest()
        {
            mockContactRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ThrowsAsync(new Exception());
            ContactDto contactDto = null;
            Assert.DoesNotThrowAsync(async () =>
            {
                contactDto = await contactService.GetContactById(Guid.NewGuid());
            });
            Assert.IsNull(contactDto);
        }

        [Test]
        public void GetContactsPositiveTest()
        {
            mockContactRepo.Setup(x => x.GetAll(It.IsAny<int>(),It.IsAny<int>())).ReturnsAsync(PhoneBookMockData.mockContacts);
            IEnumerable<ContactDto> contactDto = null;
            Assert.DoesNotThrowAsync(async () =>
            {
                contactDto = await contactService.GetContacts(0,10);
            });
            Assert.IsNotNull(contactDto);
        }

        [Test]
        public void GetContactsNegativeTest()
        {
            mockContactRepo.Setup(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception());
            IEnumerable<ContactDto> contactDto = null;
            Assert.DoesNotThrowAsync(async () =>
            {
                contactDto = await contactService.GetContacts(0, 10);
            });
            Assert.IsNull(contactDto);
        }
        [Test]
        public void GetContactsInvalidPgainationTest()
        {
            mockContactRepo.Setup(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception());
            IEnumerable<ContactDto> contactDto = null;
            Assert.DoesNotThrowAsync(async () =>
            {
                contactDto = await contactService.GetContacts(0, 0);
            });
            Assert.IsNull(contactDto);
        }

        [Test]
        public void UpdateContactPositiveTest()
        {
            mockContactRepo.Setup(x => x.Update(It.IsAny<ContactDto>())).ReturnsAsync(PhoneBookMockData.mockContact);
            ContactDto contactDto = null;
            Assert.DoesNotThrowAsync(async () =>
            {
                contactDto = await contactService.UpdateContact(PhoneBookMockData.mockContact);
            });
            Assert.IsNotNull(contactDto);
        }

        [Test]
        public void UpdateContactNegativeTest()
        {
            mockContactRepo.Setup(x => x.Update(It.IsAny<ContactDto>())).ThrowsAsync(new Exception());
            ContactDto contactDto = null;
            Assert.DoesNotThrowAsync(async () =>
            {
                contactDto = await contactService.UpdateContact(PhoneBookMockData.mockContact);
            });
            Assert.IsNull(contactDto);
        }
    }
}
