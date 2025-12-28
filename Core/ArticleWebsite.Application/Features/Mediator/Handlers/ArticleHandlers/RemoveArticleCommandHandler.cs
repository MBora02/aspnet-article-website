using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Features.Mediator.Commands.ArticleCommands;
using ArticleWebsite.Application.Interfaces;
using ArticleWebsite.Domain.Entities;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Handlers.ArticleHandlers
{
    public class RemoveArticleCommandHandler : IRequestHandler<RemoveArticleCommand>
    {
        private readonly IRepository<Article> _repository;

        public RemoveArticleCommandHandler(IRepository<Article> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveArticleCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.Id);
            await _repository.RemoveAsync(value);
        }
    }
}
