using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Features.Mediator.Commands.ReviewCommands;
using ArticleWebsite.Application.Interfaces;
using ArticleWebsite.Domain.Entities;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Handlers.ReviewHandlers
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand>
    {
        private readonly IRepository<Review> _repository;
        public UpdateReviewCommandHandler(IRepository<Review> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.ReviewId);
            values.ArticleId = request.ArticleId;
            values.ReviewerId = request.ReviewerId;
            values.Rating = request.Rating;
            values.Comment = request.Comment;

            await _repository.UpdateAsync(values);
        }
    }
}
