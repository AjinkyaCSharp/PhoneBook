using AutoMapper.Execution;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PhoneBook.Api.Test.MockData;
using PhoneBook.Controllers;
using PhoneBook.Dto;
using PhoneBook.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Api.Test.Controller
{
    public class ContactApiControllerTests
    {
        private Mock<IContactService> mockContactService;
        private Mock<ILogger<PhoneBookController>> mockPhoneBookController;
        private PhoneBookController phoneBookController;

        [SetUp]
        public void Setup()
        {
            mockContactService=new Mock<IContactService>();
            mockPhoneBookController = new Mock<ILogger<PhoneBookController>>();
            phoneBookController=new PhoneBookController(mockContactService.Object, mockPhoneBookController.Object); 
        }

        [Test]
        public void GetContactsPositiveTest()
        {
            mockContactService.Setup(x => x.GetContacts(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(PhoneBookMockData.mockContacts);
            OkObjectResult result=null;
            Assert.DoesNotThrowAsync(async () =>
            {
                result = await phoneBookController.Get(1, 2) as OkObjectResult;
            });
            var value = result.Value as IEnumerable<ContactDto>;
            Assert.IsNotNull(result);
            Assert.IsNotNull(value);
        }

        [Test]
        public void GetContactsNegativeTest()
        {
            mockContactService.Setup(x => x.GetContacts(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(null as IEnumerable<ContactDto>);
            StatusCodeResult result = null;
            Assert.DoesNotThrowAsync(async () =>
            {
                result = (await phoneBookController.Get(1, 2)) as StatusCodeResult;
            });
            Assert.IsNotNull(result);
            Assert.That(500 == (int)result.StatusCode);
        }

        [Test]
        public void GetContactNegative()
        {
            mockContactService.Setup(x => x.GetContactById(It.IsAny<Guid>())).ReturnsAsync(null as ContactDto);
            StatusCodeResult result = null;
            Assert.DoesNotThrowAsync(async () =>
            {
                result = (await phoneBookController.Get(Guid.NewGuid())) as StatusCodeResult;
            });
            Assert.IsNotNull(result);
            Assert.That(404 == result.StatusCode);
        }
        [Test]
        public void GetContactPositiveTest()
        {
            mockContactService.Setup(x => x.GetContactById(It.IsAny<Guid>())).ReturnsAsync(PhoneBookMockData.mockContact);
            OkObjectResult result = null;
            Assert.DoesNotThrowAsync(async () =>
            {
                result = (await phoneBookController.Get(Guid.NewGuid())) as OkObjectResult;
            });
            var value = result.Value as ContactDto;
            Assert.IsNotNull(result);
            Assert.IsNotNull(value);
        }

        [Test]
        public void AddContactPositiveTest() 
        {
            mockContactService.Setup(x => x.AddContact(It.IsAny<AddContactRequestDto>())).ReturnsAsync(Guid.NewGuid());
            OkObjectResult result = null;
            Assert.DoesNotThrowAsync(async () =>
            {
                result = (await phoneBookController.Post(PhoneBookMockData.mockAddContact)) as OkObjectResult;
            });
            var value = result.Value as Guid?; 
            Assert.IsNotNull(result);
            Assert.NotNull(value);
        }

        [Test]
        public void AddContactNegativeTest()
        {
            mockContactService.Setup(x => x.AddContact(It.IsAny<AddContactRequestDto>())).ReturnsAsync(null as Guid?);
            StatusCodeResult result = null;
            Assert.DoesNotThrowAsync(async () =>
            {
                result = (await phoneBookController.Post(PhoneBookMockData.mockAddContact)) as StatusCodeResult;
            });
            Assert.IsNotNull(result);
            Assert.That(500 == (int)result.StatusCode);
        }

        [Test]
        public void DeleteContactPositiveTest()
        {
            mockContactService.Setup(x => x.DeleteContactById(It.IsAny<Guid>())).ReturnsAsync(true);
            StatusCodeResult result = null;
            Assert.DoesNotThrowAsync(async () =>
            {
                result = (await phoneBookController.Delete(Guid.NewGuid())) as StatusCodeResult;
            });
            Assert.That(200 == result.StatusCode);
        }

        [Test]
        public void DeleteContactNegativeTest()
        {
            mockContactService.Setup(x => x.DeleteContactById(It.IsAny<Guid>())).ReturnsAsync(false);
            StatusCodeResult result = null;
            Assert.DoesNotThrowAsync(async () =>
            {
                result = (await phoneBookController.Delete(Guid.NewGuid())) as StatusCodeResult;
            });
            Assert.That(500 == result.StatusCode);
        }

        [Test]
        public void UpdateContactPositiveTest()
        {
            mockContactService.Setup(x => x.UpdateContact(It.IsAny<ContactDto>())).ReturnsAsync(PhoneBookMockData.mockContact);
            OkObjectResult result = null;
            Assert.DoesNotThrowAsync(async () =>
            {
                result = (await phoneBookController.Put(PhoneBookMockData.mockContact)) as OkObjectResult;
            });
            var value = result.Value as ContactDto;
            Assert.IsNotNull(result);
            Assert.IsNotNull(value);
        }

        [Test]
        public void UpdateContactNegativeTest()
        {
            mockContactService.Setup(x => x.UpdateContact(It.IsAny<ContactDto>())).ReturnsAsync(null as ContactDto);
            StatusCodeResult result = null;
            Assert.DoesNotThrowAsync(async () =>
            {
                result = (await phoneBookController.Put(PhoneBookMockData.mockContact)) as StatusCodeResult;
            });
            Assert.That(500==result.StatusCode);
        }
    }
}
