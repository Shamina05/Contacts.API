using ContactsAPI.Models;
using ContactsAPI.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.DataLayer
{
    public class ContactsDataLayer : IContactsDataLayer
    {
        private readonly ContactsContext _context;

        public ContactsDataLayer(ContactsContext contactsContext)
        {
            _context = contactsContext;
        }

        public async Task<BaseResponse<ContactsDTO>> AddNewContact(ContactsDTO contact)
        {
            var validator = new ContactsDTOValidator();
            var result = validator.Validate(contact);

            if (!result.IsValid)
            {
                return new BaseResponse<ContactsDTO>()
                {
                    ErrorStatus = ErrorStatus.Error,
                    ErrorInfo = new ErrorInfo()
                    {
                        ErrorMessage = result.Errors.First().ErrorMessage
                    }
                };
            }

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return new BaseResponse<ContactsDTO>()
            {
                ErrorStatus = ErrorStatus.Success,
                Result = contact
            };
        }

        public Task<bool> CheckContactExists(int id)
        {
            return _context.Contacts.AnyAsync(i => i.ContactId == id);
        }

        public async Task<int> DeleteContact(int id)
        {
            var contactData = await _context.Contacts.FirstOrDefaultAsync(i => i.ContactId == id);

            if (contactData != null)
            {
                _context.Contacts.Remove(contactData);
                _context.Remove(contactData).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            else
            {
                return (int)ErrorStatus.Failed;
            }

            return (int)ErrorStatus.Success;
        }

        public async Task<List<ContactsDTO>> GetAllContactsAsync()
        {
            var result = new List<ContactsDTO>();
            var dataset = await _context.Contacts.OrderBy(i => i.ContactId).ToListAsync();

            if (dataset != null)
            {

                foreach (var item in dataset)
                {
                    ContactsDTO contact = new ContactsDTO();
                    contact.ContactId = item.ContactId;
                    contact.FirstName = item.FirstName;
                    contact.LastName = item.LastName;
                    contact.Telephone = item.Telephone;
                    contact.Mobile = item.Mobile;
                    contact.Email = item.Email;
                    contact.DateCreated = item.DateCreated;
                    contact.DateUpdated = item.DateUpdated;
                    result.Add(contact);
                }             
            }
            return result;

        }


        public Task<ContactsDTO> GetContactByIdAsync(int id)
        {
            return _context.Contacts.FirstOrDefaultAsync(i => i.ContactId == id);
        }

        public async Task<BaseResponse<ContactsDTO>> UpdateContactAsync(int id, ContactsDTO contact)
        {
            var validator = new ContactsDTOValidator();
            var result = validator.Validate(contact);

            if (!result.IsValid)
            {
                return new BaseResponse<ContactsDTO>()
                {
                    ErrorStatus = ErrorStatus.Error,
                    ErrorInfo = new ErrorInfo()
                    {
                        ErrorMessage = result.Errors.First().ErrorMessage
                    }
                };
            }

            var contactData = await _context.Contacts.FirstOrDefaultAsync(i => i.ContactId == id);

            if (contactData != null)
            {
                contactData.DateUpdated = DateTime.Now;
                contactData.LastName = contact.LastName;
                contactData.Telephone = contact.Telephone;
                contactData.Mobile = contact.Mobile;
                contactData.Email = contact.Email;
                _context.Contacts.Attach(contactData);
                _context.Entry(contactData).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new BaseResponse<ContactsDTO>()
                {
                    ErrorStatus = ErrorStatus.Success,
                    Result = contact
                };
            }
            else
            {
                return new BaseResponse<ContactsDTO>()
                {
                    ErrorStatus = ErrorStatus.Failed,
                    ErrorInfo = new ErrorInfo()
                    {
                        ErrorMessage = "Contact Not Found"
                    },
                    Result = null
                };
            }
           
        }
    }
}