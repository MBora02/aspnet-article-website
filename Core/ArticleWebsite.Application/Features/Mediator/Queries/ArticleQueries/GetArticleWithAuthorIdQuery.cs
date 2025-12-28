using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Features.Mediator.Results.ArticleResults;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Queries.ArticleQueries
{
    public class GetArticleWithAuthorIdQuery:IRequest<List<GetArticleWithAuthorIdQueryResult>>
    {
        public int AuthorId { get; set; }

        public GetArticleWithAuthorIdQuery(int authorid)
        {
            AuthorId = authorid;
        }
    }
}
