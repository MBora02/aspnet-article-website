using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Features.Mediator.Results.ArticleResults;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Queries.ArticleQueries
{
    public class GetArticleByIdQuery:IRequest<GetArticleByIdQueryResult>
    {
        public int Id { get; set; }

        public GetArticleByIdQuery(int id)
        {
            Id = id;
        }
    }
}
