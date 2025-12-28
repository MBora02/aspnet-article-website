using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Features.Mediator.Results.ArticleResults;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Queries.ArticleQueries
{
    public class GetArticleByDepartmentIdQuery:IRequest<List<GetArticleByDepartmentIdQueryResult>>
    {
        public int Id { get; set; }

        public GetArticleByDepartmentIdQuery(int id)
        {
            Id = id;
        }
    }
}
