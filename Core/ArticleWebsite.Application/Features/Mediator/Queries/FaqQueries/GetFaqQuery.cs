using ArticleWebsite.Application.Features.Mediator.Results.FaqResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Queries.FaqQueries
{
    public class GetFaqQuery:IRequest<List<GetFaqQueryResult>>
    {
    }
}
