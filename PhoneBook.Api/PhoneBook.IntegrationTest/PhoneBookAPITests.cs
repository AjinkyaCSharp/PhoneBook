using Azure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using PhoneBook.Database;
using PhoneBook.Database.Model;
using PhoneBook.Dto;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PhoneBook.IntegrationTest
{
    public class PhoneBookAPITests
    {
        HttpClient client;
        WebApplicationFactory<Program> webApplicationFactory;
        PhoneBookDbContext phoneBookDbContext;
        IDbContextTransaction transaction;
        JsonSerializerOptions jsonSerializerOptions;
        [SetUp]
        public void Setup()
        {
            webApplicationFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(webHostBuilder =>
            {

            });
            client = webApplicationFactory.CreateClient();
            phoneBookDbContext = new PhoneBookDbContext();
            transaction = phoneBookDbContext.Database.BeginTransaction();
            jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }
        [TearDown]
        public void TearDown()
        {
            transaction.Rollback();
        }

        [Test]
        public async Task GetContactsTest()
        {
            var httpResult = await client.GetAsync("/api/phonebook/contact?offset=0&numRecords=5");
            Assert.IsNotNull(httpResult);
            Assert.True(httpResult.IsSuccessStatusCode);
            var stringResponse = (await httpResult.Content.ReadAsStringAsync());
            var response = JsonSerializer.Deserialize<List<ContactDto>>(stringResponse, jsonSerializerOptions);
            Assert.IsNotNull(response);
            Assert.True(response.Count() == 1); 
        }

        [Test]
        public async Task AddContactTest()
        {
            var httpResult = await AddContact();
            Assert.IsNotNull(httpResult);
            Assert.IsTrue(httpResult.IsSuccessStatusCode);
            var response = JsonSerializer.Deserialize<Guid>(await httpResult.Content.ReadAsStringAsync());
            var dbContact = phoneBookDbContext.Contacts.Where(x => x.Id == response).FirstOrDefault();
            Assert.IsNotNull(dbContact);
        }

        [Test]
        public async Task GetContactTest()
        {
            var httpResult = await AddContact();
            Assert.IsNotNull(httpResult);
            Assert.IsTrue(httpResult.IsSuccessStatusCode);

            var response = JsonSerializer.Deserialize<Guid>(await httpResult.Content.ReadAsStringAsync());
            var httpGetResult = await client.GetAsync($"/api/phonebook/contact/id/{response}");
            Assert.IsNotNull(httpGetResult);
            Assert.IsTrue(httpGetResult.IsSuccessStatusCode);
            var contact = JsonSerializer.Deserialize<ContactDto>((await httpGetResult.Content.ReadAsStringAsync()));
            Assert.NotNull(contact);
            Assert.That(response, Is.EqualTo(contact.Id));
        }

        [Test]
        public async Task UpdateContactTest()
        {
            var response = await AddContact();
            var contactId = JsonSerializer.Deserialize<Guid>(await response.Content.ReadAsStringAsync());
            var editConact = new ContactDto
            {
                Id = contactId,
                FirstName = "Habibi",
                LastName = "Sheikh",
                Address = "Kuwait",
                Email = "habibi@lotofmoney.com",
                PrimaryContact = "9999999999",
                SecondaryContact = "9999999999"
            };
            var httpResult= await client.PutAsJsonAsync<ContactDto>("/api/phonebook/contact", editConact);
            Assert.NotNull(httpResult);
            Assert.IsTrue(httpResult.IsSuccessStatusCode);
            var dbcontact=phoneBookDbContext.Contacts.Where(x=>x.Id== contactId).FirstOrDefault();
            Assert.That(dbcontact.Address, Is.EqualTo(editConact.Address));
        }
        [Test]
        public async Task DeleteContactTest()
        {
            var response = await AddContact();
            var contactId = JsonSerializer.Deserialize<Guid>(await response.Content.ReadAsStringAsync());

            var httpResult = await client.DeleteAsync($"/api/phonebook/contact/{contactId}");
            Assert.NotNull(httpResult);
            Assert.IsTrue(httpResult.IsSuccessStatusCode);
            var dbRecord = phoneBookDbContext.Contacts.Where(x=>x.Id==contactId).FirstOrDefault();
            Assert.IsNull(dbRecord);

        }
        private async Task<HttpResponseMessage> AddContact()
        {
            var contact = new AddContactRequestDto
            {
                FirstName = "Habibi",
                LastName = "Sheikh",
                Address = "Oil well",
                Email = "habibi@lotofmoney.com",
                PrimaryContact = "9999999999",
                SecondaryContact = "9999999999"
            };
            return await client.PostAsJsonAsync<AddContactRequestDto>("/api/phonebook/contact", contact);
        }
    }
}