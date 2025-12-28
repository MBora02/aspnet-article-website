using ArticleWebsite.Application.Features.Mediator.Queries.ReviewQueries;
using ArticleWebsite.Application.Features.Mediator.Results.ReviewResults;
using ArticleWebsite.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Handlers.ReviewHandlers
{
    public class GetReviewByAppUserIdQueryHandler : IRequestHandler<GetReviewByAppUserIdQuery, List<GetReviewByAppUserIdQueryResult>>
    {
        private readonly IReviewRepository _repository;

        public GetReviewByAppUserIdQueryHandler(IReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetReviewByAppUserIdQueryResult>> Handle(GetReviewByAppUserIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetReviewByAppUserIdAsync(request.Id);
            return values.Select(x => new GetReviewByAppUserIdQueryResult()
            {
                ArticleId = x.ArticleId,
                CreatedAt = x.CreatedAt,
                Rating = x.Rating,
                Comment = x.Comment,
                ReviewId = x.ReviewId,
                ReviewerId = x.ReviewerId,
                ReviewerName = x.Reviewer?.Name,
                ReviewerSurname = x.Reviewer?.Surname,
                ArticleTitle = x.Article?.Title
            }).ToList();
        }
    }
}
