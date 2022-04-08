using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage("Alıcı Adresi Boş Geçilemez");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konuyu Boş Geçilemez");
            RuleFor(x => x.MessageContent).NotEmpty().WithMessage("Mesajı Boş Geçilemez");
            RuleFor(x => x.Subject).MinimumLength(3).WithMessage("Lütfen en az 3 karakter girişi yapın");
            RuleFor(x => x.Subject).MaximumLength(100).WithMessage("Lütfen 100 karakterden fazla karakter girişi yapmayın");
        }
        
    }
}
