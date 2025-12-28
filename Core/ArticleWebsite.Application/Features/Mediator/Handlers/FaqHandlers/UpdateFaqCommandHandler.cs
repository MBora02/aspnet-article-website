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
    public class UpdateFaqCommandHandler : IRequestHandler<UpdateFaqCommand>
    {
        private readonly IRepository<Faq> _repository;

        public UpdateFaqCommandHandler(IRepository<Faq> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateFaqCommand request, CancellationToken cancellationToken)
        {
            var values=await _repository.GetByIdAsync(request.FaqId);
            values.Question=request.Question;
            values.Answer=request.Answer;
            await _repository.UpdateAsync(values);
        }
    }
}
