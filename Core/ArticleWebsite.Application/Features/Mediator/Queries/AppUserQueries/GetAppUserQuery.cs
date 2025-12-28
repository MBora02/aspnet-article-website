using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Features.Mediator.Results.AppUserResults;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Queries.AppUserQueries
{
    public class GetAppUserQuery:IRequest<List<GetAppUserQueryResult>>
    {
    }
}
