using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Features.Mediator.Results.DepartmentResults;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Queries.DepartmentQueries
{
    public class GetDepartmentQuery:IRequest<List<GetDepartmentQueryResult>>
    {
    }
}
