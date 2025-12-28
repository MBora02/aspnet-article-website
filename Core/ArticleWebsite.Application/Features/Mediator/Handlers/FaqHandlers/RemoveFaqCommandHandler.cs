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
    public class RemoveFaqCommandHandler : IRequestHandler<RemoveFaqCommand>
    {
        private readonly IRepository<Faq> _repository;

        public RemoveFaqCommandHandler(IRepository<Faq> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveFaqCommand request, CancellationToken cancellationToken)
        {
            var values=await _repository.GetByIdAsync(request.Id);
            await _repository.RemoveAsync(values);
        }
    }
}
