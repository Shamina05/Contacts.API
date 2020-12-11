using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Models
{
    public class ContactsContext: DbContext
    {
        public ContactsContext(DbContextOptions<ContactsContext> options) : base(options)
        {

        }
        public DbSet<ContactsDTO> Contacts { get; set; }
    }
}
