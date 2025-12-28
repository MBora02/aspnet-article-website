using ArticleWebsite.Application.Features.Mediator.Results.ArticleResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Queries.ArticleQueries
{
    public class GetArticleByTagCloudIdQuery : IRequest<List<GetArticleByTagCloudIdQueryResult>>
    {
        public int Id { get; set; }

        public GetArticleByTagCloudIdQuery(int id)
        {
            Id = id;
        }
    }
}
