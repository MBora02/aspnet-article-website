using ArticleWebsite.Application.Features.Mediator.Results.AppRoleResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Queries.AppRoleQueries
{
    public class GetAppRoleQuery : IRequest<List<GetAppRoleQueryResult>>
    {
    }
}
