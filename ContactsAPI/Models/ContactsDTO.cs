using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsAPI.Models
{
    public class ContactsDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }

        [StringLength(25), Required]

        public string FirstName { get; set; }
        [StringLength(25), Required]
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Telephone Number")]
        public string Telephone { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Mobile Number")]
        public string Mobile { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
