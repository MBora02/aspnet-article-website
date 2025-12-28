using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Features.Mediator.Queries.ReviewQueries;
using ArticleWebsite.Application.Features.Mediator.Results.ReviewResults;
using ArticleWebsite.Application.Interfaces;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Handlers.ReviewHandlers
{
    public class GetReviewByArticleIdQueryHandler : IRequestHandler<GetReviewByArticleIdQuery, List<GetReviewByArticleIdQueryResult>>
    {
        private readonly IReviewRepository _repository;

        public GetReviewByArticleIdQueryHandler(IReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetReviewByArticleIdQueryResult>> Handle(GetReviewByArticleIdQuery request, CancellationToken cancellationToken)
        {
            var values= await _repository.GetReviewByArticleIdAsync(request.Id);
            return values.Select(x=> new GetReviewByArticleIdQueryResult()
            {
                ArticleId=x.ArticleId,
                CreatedAt = x.CreatedAt,
                Rating = x.Rating,
                Comment=x.Comment,
                ReviewId=x.ReviewId,
                ReviewerId = x.ReviewerId,
                ReviewerName=x.Reviewer?.Name,
                ReviewerSurname=x.Reviewer?.Surname,
                ArticleTitle=x.Article?.Title
            }).ToList();
        }
    }
}
