using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinnessLayer.ValidationRules
{
    public class ContactValidator:AbstractValidator<Contact>
    {
        public ContactValidator() {
            RuleFor(c => c.Subject).NotEmpty().WithMessage("Konu Adını boş geçemezsiniz...");
            RuleFor(c => c.UserName).NotEmpty().WithMessage("Kullancı Adını boş geçemezsiniz...");
            RuleFor(c => c.Subject).MinimumLength(3).WithMessage("Lütfen en az 3 karakter girişi yapın...");
            RuleFor(c => c.UserMail).NotEmpty().WithMessage("Konu Adını boş geçemezsiniz...");
            RuleFor(c => c.UserName).MinimumLength(3).WithMessage("Lütfen en az 3 karakter girişi yapın...");
            RuleFor(c => c.Subject).MaximumLength(50).WithMessage("Lütfen en az 50 karakter girişi yapın...");
        }
    }
}
