using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinnessLayer.ValidationRules
{
    public class MessageValidator:AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(m=>m.ReceiverMail).NotEmpty().WithMessage("Alıcı mail adresi boş geçilemez...");
            RuleFor(m=>m.Subject).NotEmpty().WithMessage("Konuyu boş geçilemez...");
            RuleFor(m=>m.MessageContent).NotEmpty().WithMessage("Mesaj içeriği boş geçilemez...");
            RuleFor(m => m.ReceiverMail).EmailAddress().WithMessage("Geçersiz email adresi...");
            RuleFor(m => m.Subject).MinimumLength(3).WithMessage("Konu 3 karakterden daha az olamaz...");
            RuleFor(m => m.Subject).MaximumLength(100).WithMessage("Lütfen 100 karakterden fazla giriş yapmayınız...");
        }
    }
}
