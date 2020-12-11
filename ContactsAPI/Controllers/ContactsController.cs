using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactsAPI.Models;

namespace ContactsAPI.Controllers
{
    [Route("api/Contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IContactsDataLayer dataLayer;

        public ContactsController(IContactsDataLayer _dataLayer)
        {
           this.dataLayer = _dataLayer;
        }

        [HttpGet("GetAllContactsAsync")]
        public async Task<ActionResult<IEnumerable<ContactsDTO>>> GetAllContactsAsync()
        {
            var result =  await dataLayer.GetAllContactsAsync();

            if(result.Count > 0)
            {
                return result;
            }
            else
            {
                return NotFound("No Contacts Found");
            }
        }

        [HttpGet("GetContactByIdAsync/{id}")]
        public async Task<ActionResult<ContactsDTO>> GetContactByIdAsync(int id)
        {
            var contacts = await dataLayer.GetContactByIdAsync(id);

            if (contacts == null)
            {
                return NotFound();
            }

            return contacts;
        }

        [HttpPut("UpdateContactAsync/{id}")]
        public async Task<IActionResult> UpdateContactAsync(int id, ContactsDTO contacts)
        {
            if (id != contacts.ContactId)
            {
                return BadRequest();
            }

            try
            {
               var result = await dataLayer.UpdateContactAsync(id, contacts);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await ContactsExists(id)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok("Contact Updated Successfully");
        }

        [HttpPost("AddNewContact")]
        public async Task<ActionResult<BaseResponse<ContactsDTO>>> AddNewContact(ContactsDTO contacts)
        {

            return await dataLayer.AddNewContact(contacts);
        }

        [HttpDelete("DeleteContactById/{id}")]
        public async Task<IActionResult> DeleteContactById(int id)
        {
            int contactUpdated = await dataLayer.DeleteContact(id);
            if (contactUpdated == (int)ErrorStatus.Failed)
            {
                return NotFound();
            }

            return Ok("Contact Deleted Successfully");
        }

        private Task<bool> ContactsExists(int id)
        {
            return dataLayer.CheckContactExists(id);

        }
    }
}
