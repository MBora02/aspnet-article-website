using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Features.Mediator.Commands.ReviewCommands;
using FluentValidation;

namespace ArticleWebsite.Application.Validators
{
    public class CreateReviewValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewValidator()
        {
            RuleFor(x => x.Rating).NotEmpty().WithMessage("Lütfen puan değerini boş geçmeyiniz");
        }
    }
}
