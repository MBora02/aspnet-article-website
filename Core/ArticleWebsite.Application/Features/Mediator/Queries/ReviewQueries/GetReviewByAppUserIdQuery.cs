using ArticleWebsite.Application.Features.Mediator.Results.ReviewResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Queries.ReviewQueries
{
    public class GetReviewByAppUserIdQuery:IRequest<List<GetReviewByAppUserIdQueryResult>>
    {
        public int Id { get; set; }

        public GetReviewByAppUserIdQuery(int id)
        {
            Id = id;
        }
    }
}
