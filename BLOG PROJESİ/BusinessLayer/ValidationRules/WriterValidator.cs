﻿using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator:AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Yazar Adı Boş Geçilemez.");
            RuleFor(x => x.WriterSurName).NotEmpty().WithMessage("Yazar Soy Boş Geçilemez.");
            RuleFor(x => x.WriterTitle).NotEmpty().WithMessage("Ünvan Kısmı Boş Geçilemez.");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Hakkında kısmı boş geçilemez.");
            RuleFor(x => x.WriterSurName).MinimumLength(2).WithMessage("Lütfen en az 3 karakter girişi yapınız.");
            RuleFor(x => x.WriterSurName).MaximumLength(50).WithMessage("Lütfen 50 karakterden fazla değer girişi yapmayınız.");
        }
    }
}
