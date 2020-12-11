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
    public class GetContactById_Test
    {
        private ContactsContext contactsContext;
        private IContactsDataLayer dataLayer;
        private int validContactId = 2;
        public int invalidContactId = 5;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ContactsContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;
            contactsContext = new ContactsContext(options);
            dataLayer = new ContactsDataLayer(contactsContext);
        }

        [Test]
        public void When_Requesting_Contact_With_Valid_ID_Return_Contact()
        {
            //Arrange
            var contact = new ContactsDTO()
            {
                ContactId = validContactId, 
                FirstName = "Shamina", 
                LastName = "Maharaj", 
                Telephone = "0325516332", 
                Mobile = "0829519581", 
                Email = null, 
                DateCreated = DateTime.Now, 
                DateUpdated = DateTime.Now,
            };

            contactsContext.Contacts.Add(contact);
            contactsContext.SaveChanges();


            //Act
            var result = dataLayer.GetContactByIdAsync(validContactId).Result;

            //Assert
            Assert.AreSame(result, contact);
        }

        [Test]
        public async Task When_Requesting_Contact_With_Invalid_ID_Return_Null()
        {
            //Arrange
            var contact = new ContactsDTO()
            {
                ContactId = validContactId,
                FirstName = "Shamina",
                LastName = "Maharaj",
                Telephone = "0325516332",
                Mobile = "0829519581",
                Email = null,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
            };

            contactsContext.Contacts.Add(contact);
            contactsContext.SaveChanges();

            //Act
            var result = await dataLayer.GetContactByIdAsync(invalidContactId);

            //Assert
            Assert.AreSame(result, null);
        }
    }
}
