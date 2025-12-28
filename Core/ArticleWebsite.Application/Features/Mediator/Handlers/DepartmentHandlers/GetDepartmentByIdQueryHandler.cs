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
    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, GetDepartmentByIdQueryResult>
    {
        private readonly IRepository<Department> _repository;
        public GetDepartmentByIdQueryHandler(IRepository<Department> repository)
        {
            _repository = repository;
        }

        public async Task<GetDepartmentByIdQueryResult> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            return new GetDepartmentByIdQueryResult
            {
                DepartmentId = values.DepartmentId,
                Name = values.Name
            };
        }
    }
}
