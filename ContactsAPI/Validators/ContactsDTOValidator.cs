using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsAPI.Models;
using FluentValidation;

namespace ContactsAPI.Validators
{
    public class ContactsDTOValidator : AbstractValidator<ContactsDTO>
    {
        public ContactsDTOValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName Cannot Be Empty!");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName Cannot Be Empty!");
            RuleFor(x => x.Mobile).MaximumLength(10).WithMessage("Mobile Number Must Be 10 Digits");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email Address Invalid!");
           
        }
    }
}
