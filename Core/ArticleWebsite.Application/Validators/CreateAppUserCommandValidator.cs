using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Features.Mediator.Commands.AppUserCommands;
using FluentValidation;

namespace ArticleWebsite.Application.Validators
{
    public class CreateAppUserCommandValidator : AbstractValidator<CreateAppUserCommand>
    {
        public CreateAppUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Surname).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password)
     .NotEmpty().WithMessage("Şifre alanı boş bırakılamaz.")
     .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
     .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
     .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
     .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.")
     .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir.");

            RuleFor(x => x.DepartmentId).GreaterThan(0);
            RuleFor(x => x.Email)
    .NotEmpty()
    .EmailAddress()
    .Matches(@"@bozok\.edu\.tr$")
    .WithMessage("Sadece @bozok.edu.tr uzantılı mail adreslerine izin verilir.");
        }
    }
}
