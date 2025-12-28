using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Features.Mediator.Queries.DepartmentQueries;
using ArticleWebsite.Application.Features.Mediator.Results.DepartmentResults;
using ArticleWebsite.Application.Interfaces;
using ArticleWebsite.Domain.Entities;
using MediatR;

namespace ArticleWebsite.Application.Departments.Mediator.Handlers.DepartmentHandlers
{
    public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, List<GetDepartmentQueryResult>>
    {
        private readonly IRepository<Department> _repository;
        public GetDepartmentQueryHandler(IRepository<Department> repository)
        {
            _repository = repository;
        }
        public async Task<List<GetDepartmentQueryResult>> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetDepartmentQueryResult
            {
                DepartmentId = x.DepartmentId,
                Name = x.Name
            }).ToList();
        }
    }
}
