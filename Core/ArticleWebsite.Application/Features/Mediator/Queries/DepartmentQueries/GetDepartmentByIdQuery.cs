using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Features.Mediator.Results.DepartmentResults;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Queries.DepartmentQueries
{
    public class GetDepartmentByIdQuery:IRequest<GetDepartmentByIdQueryResult>
    {
        public int Id { get; set; }

        public GetDepartmentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
