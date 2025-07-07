using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinnessLayer.ValidationRules
{
    public class HeadingValidator: AbstractValidator<Heading>
    {
        public HeadingValidator()
        {
            // HeadingID: Genellikle otomatik artan bir ID olduğu için doğrulama gereksiz.
            // Ama eğer elle değer veriliyorsa, pozitif bir sayı olmalıdır.
            RuleFor(x => x.HeadingID)
                .GreaterThan(0).WithMessage("HeadingID pozitif bir değer olmalıdır.");

            // HeadingName: Boş olmamalı ve 50 karakteri geçmemeli.
            RuleFor(x => x.HeadingName)
                .NotEmpty().WithMessage("Başlık adı boş olamaz.")
                .Length(1, 30).WithMessage("Başlık adı en fazla 30 karakter olabilir.");

            // HeadingDate: Geçerli bir tarih olmalı. Eğer güncel tarihi istiyorsanız bu tür bir kısıtlama da eklenebilir.
            RuleFor(x => x.HeadingDate)
                .NotEmpty().WithMessage("Başlık tarihi boş olamaz.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Başlık tarihi gelecekte olamaz.");

            // HeadingStatus: Başlık durumu bir boolean olduğu için özel bir doğrulama gerekmez, ancak geçerli bir değer olmalıdır.
            RuleFor(x => x.HeadingStatus)
                .NotNull().WithMessage("Başlık durumu geçerli bir değer olmalıdır.");

            // CategoryId: Geçerli bir kategori ID'si olmalı.
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Geçerli bir kategori ID'si seçmelisiniz.");

            // WriterID: Geçerli bir yazar ID'si olmalı.
            RuleFor(x => x.WriterID)
                .GreaterThan(0).WithMessage("Geçerli bir yazar ID'si seçmelisiniz.");

            // Contents: İçerik koleksiyonu boş olmamalı.
            RuleFor(x => x.Contents)
                .NotEmpty().WithMessage("Başlık altında en az bir içerik olmalıdır.");
        }
    }
}
