using ArticleWebsite.Application.Features.Mediator.Commands.ReviewCommands;
using ArticleWebsite.Application.Interfaces;
using ArticleWebsite.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Handlers.ReviewHandlers
{
    public class RemoveReviewCommandHandler : IRequestHandler<RemoveReviewCommand>
    {
        private readonly IRepository<Review> _repository;

        public RemoveReviewCommandHandler(IRepository<Review> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveReviewCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.Id);
            await _repository.RemoveAsync(value);
        }
    }
}
