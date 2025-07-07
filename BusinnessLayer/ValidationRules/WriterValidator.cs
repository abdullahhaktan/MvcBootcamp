using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinnessLayer.ValidationRules
{
    public class WriterValidator:AbstractValidator<Writer>
    {
        public WriterValidator()
        { 
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Kategori Adını Boş Geçemezsiniz...");
            RuleFor(x => x.WriterName).MinimumLength(3).WithMessage("Lütfen en az 3 karakter girişi yapınız...");
            RuleFor(x => x.WriterName).MaximumLength(20).WithMessage("Lütfen en fazla 20 karakter girişi yapınız...");
            RuleFor(x => x.WriterSurname).NotEmpty().WithMessage("Yazar Soy Adını Boş Geçemezsiniz...");
            RuleFor(x => x.WriterSurname).MinimumLength(3).WithMessage("Lütfen en az 3 karakter girişi yapınız...");
            RuleFor(x => x.WriterSurname).MaximumLength(20).WithMessage("Lütfen en fazla 20 karakter girişi yapınız...");
            RuleFor(x => x.WriterMail).NotEmpty().WithMessage("Mail kısmını boş geçemezsiniz...");
            RuleFor(x => x.WriterTitle).NotEmpty().WithMessage("Başlık kısmını boş geçemezsiniz...");
            RuleFor(w => w.WriterAbout).Matches(".*[aA].*").WithMessage("Yazar Adı 'a' veya 'A' harfi içermelidir...");
        }
    }
}
