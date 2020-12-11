using ContactsAPI;
using ContactsAPI.DataLayer;
using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Test.ContactsTests
{
    public class UpdateContactDetails_Test
    {
        private ContactsContext contactsContext;
        private IContactsDataLayer dataLayer;
        private int nonUpdatedContactId = 3;
        private DateTime unUpdatedDate = DateTime.Now.AddDays(-1);
        private ContactsDTO contacts;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ContactsContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;
            contactsContext = new ContactsContext(options);
            dataLayer = new ContactsDataLayer(contactsContext);

            contacts = new ContactsDTO()
            {
                ContactId = nonUpdatedContactId,
                FirstName = "Shamina",
                LastName = "Maharaj",
                Telephone = "0325516332",
                Mobile = "0829519581",
                Email = "maharajshamina1@gmail.com",
                DateCreated = unUpdatedDate,
                DateUpdated = unUpdatedDate
            };
            contactsContext.Contacts.Add(contacts);
            contactsContext.SaveChanges();
        }

        [Test]
        public void No_Update_Made_If_ContactId_Does_Not_Exist()
        {
            //Arrange
            var incorrectContactId = nonUpdatedContactId + 5;

            //Act
            var result = dataLayer.UpdateContactAsync(incorrectContactId, contacts).Result;

            //Assert
            Assert.IsNull(result.Result); //Not found, should be null
            Assert.AreSame(contacts, contactsContext.Contacts.First());
            Assert.True(result.ErrorStatus == ErrorStatus.Failed);
            Assert.AreEqual(result.ErrorInfo.ErrorMessage, "Contact Not Found");
        }

        [Test]
        public void Given_Valid_Id_Update_Contact()
        {
            //Arrange
            ContactsDTO updatedContact = new ContactsDTO();

            updatedContact = new ContactsDTO()
            {
                ContactId = nonUpdatedContactId,
                FirstName = "Shamina",
                LastName = "Singh",
                Telephone = "0325523816",
                Mobile = "0728788180",
                Email = "ShMaharaj@mrpg.com",
                DateCreated = unUpdatedDate,
                DateUpdated = DateTime.Now
            };

            //Act
            var result = dataLayer.UpdateContactAsync(nonUpdatedContactId, updatedContact).Result;

            //Assert
            var dbContact = contactsContext.Contacts.First();
            Assert.AreEqual(result.Result.ContactId, dbContact.ContactId);
            Assert.AreSame(result.Result.FirstName, dbContact.FirstName);
            Assert.AreSame(result.Result.LastName, dbContact.LastName);
            Assert.AreSame(result.Result.Telephone, dbContact.Telephone);
            Assert.AreSame(result.Result.Mobile, dbContact.Mobile);
            Assert.AreSame(result.Result.Email, dbContact.Email);
            Assert.AreEqual(result.Result.DateCreated, dbContact.DateCreated);
            Assert.AreNotSame(result.Result.DateUpdated, unUpdatedDate);
        }

    }
}
