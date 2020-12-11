using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Moq;
using ContactsAPI.DataLayer;
using ContactsAPI.Models;
using NUnit.Framework;
using ContactsAPI;

namespace Contacts.Test.ContactsTests
{
    public class CreateContactDTO_Test
    {
        private ContactsContext contactsContext;
        private IContactsDataLayer dataLayer;

        [Test]
        public async Task Information_Needs_To_Be_Correctly_InsertedAsync()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ContactsContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            contactsContext = new ContactsContext(options);
            dataLayer = new ContactsDataLayer(contactsContext);

            var contacts = new ContactsDTO()
            {
                ContactId = 1,
                FirstName = "Shamina",
                LastName = "Maharaj",
                Telephone = "0325516332",
                Mobile = "0829519581",
                Email = "maharajshamina1@gmail.com",
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
            };

            //Act

            BaseResponse<ContactsDTO> result = await dataLayer.AddNewContact(contacts);

            //Assert
            Assert.NotNull(result.Result);
            Assert.AreEqual(result.ErrorStatus, ErrorStatus.Success);
            Assert.AreEqual(result.Result.ContactId, contacts.ContactId);
            Assert.AreEqual(result.Result.FirstName, contacts.FirstName);
            Assert.AreEqual(result.Result.LastName, contacts.LastName);
            Assert.AreEqual(result.Result.Telephone, contacts.Telephone);
            Assert.AreEqual(result.Result.Mobile, contacts.Mobile);
            Assert.AreEqual(result.Result.Email, contacts.Email);
            Assert.AreEqual(result.Result.DateCreated, contacts.DateCreated);
            Assert.AreEqual(result.Result.DateUpdated, contacts.DateUpdated);

        }


        [Test]
        public async Task When_Invalid_Information_Inserted_Return_Error()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ContactsContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            contactsContext = new ContactsContext(options);
            dataLayer = new ContactsDataLayer(contactsContext);

            var contacts = new ContactsDTO()
            {
                ContactId = 1,
                FirstName = "Shamina",
                LastName = "Maharaj",
                Telephone = "032abcs",
                Mobile = "12345po",
                Email = "maharajshamina1gmailcom",
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
            };

            //Act

            BaseResponse<ContactsDTO> result = await dataLayer.AddNewContact(contacts);

            //Assert
            Assert.AreEqual(result.Result, null);
            Assert.AreEqual(result.ErrorStatus, ErrorStatus.Error);
        }

    }


}
