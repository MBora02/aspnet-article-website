using ArticleWebsite.Application.Features.Mediator.Commands.FaqCommands;
using ArticleWebsite.Application.Interfaces;
using ArticleWebsite.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Handlers.FaqHandlers
{
    public class CreateFaqCommandHandler : IRequestHandler<CreateFaqCommand>
    {
        private readonly IRepository<Faq> _repository;

        public CreateFaqCommandHandler(IRepository<Faq> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateFaqCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Faq
            {
                Question = request.Question,
                Answer = request.Answer
            });
        }
    }
}
