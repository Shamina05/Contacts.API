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
    public class DeleteContactById
    {
        private ContactsContext contactsContext;
        private IContactsDataLayer dataLayer;
        private ContactsDTO contacts;
        private int correctId = 2;

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
                ContactId = correctId,
                FirstName = "Shamina",
                LastName = "Maharaj",
                Telephone = "0325516332",
                Mobile = "0829519581",
                Email = "maharajshamina1@gmail.com",
                DateCreated = DateTime.Now.AddDays(-1),
                DateUpdated = DateTime.Now
            };
            contactsContext.Contacts.Add(contacts);
            contactsContext.SaveChanges();
        }

        [Test]
        public void Given_Invalid_ID_Return_Failed()
        {
            //Arrange
            var incorrectContactId = correctId + 3;

            //Act
            var result = dataLayer.DeleteContact(incorrectContactId).Result;

            //Assert
            Assert.AreEqual(result, (int)ErrorStatus.Failed); 
        }

        [Test]
        public void Given_Valid_ID_Return_Success()
        {
            //Arrange

            //Act
            var result = dataLayer.DeleteContact(correctId).Result;

            //Assert
            Assert.AreEqual(result, (int)ErrorStatus.Success);
        }
    }
}
