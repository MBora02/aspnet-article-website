using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Features.Mediator.Results.ReviewResults;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Queries.ReviewQueries
{
    public class GetReviewByArticleIdQuery : IRequest<List<GetReviewByArticleIdQueryResult>>
    {
        public int Id { get; set; }

        public GetReviewByArticleIdQuery(int id)
        {
            Id = id;
        }
    }
}
