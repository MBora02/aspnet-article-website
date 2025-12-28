using ArticleWebsite.Application.Features.Mediator.Results.FaqResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Queries.FaqQueries
{
    public class GetFaqByIdQuery:IRequest<GetFaqByIdQueryResult>
    {
        public int Id { get; set; }

        public GetFaqByIdQuery(int id)
        {
            Id = id;
        }
    }
}
