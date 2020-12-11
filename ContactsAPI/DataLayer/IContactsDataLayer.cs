using ContactsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI
{
    public interface IContactsDataLayer
    {
        Task<List<ContactsDTO>> GetAllContactsAsync();

        Task<ContactsDTO> GetContactByIdAsync(int id);

        Task<BaseResponse<ContactsDTO>> UpdateContactAsync(int id, ContactsDTO contact);
        Task<BaseResponse<ContactsDTO>> AddNewContact(ContactsDTO contact);
        Task<int> DeleteContact(int id);
        Task<bool> CheckContactExists(int id);
    }
}
