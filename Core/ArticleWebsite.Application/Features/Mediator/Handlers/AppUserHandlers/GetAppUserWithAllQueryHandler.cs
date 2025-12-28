using ArticleWebsite.Application.Features.Mediator.Queries.AppUserQueries;
using ArticleWebsite.Application.Features.Mediator.Results.AppUserResults;
using ArticleWebsite.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Handlers.AppUserHandlers
{
    internal class GetAppUserWithAllQueryHandler : IRequestHandler<GetAppUserWithAllQuery, List<GetAppUserWithAllQueryResult>>
    {
        private readonly IAppUserRepository _repository;

        public GetAppUserWithAllQueryHandler(IAppUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<GetAppUserWithAllQueryResult>> Handle(GetAppUserWithAllQuery request, CancellationToken cancellationToken)
        {
            var users = _repository.GetAppUsersWithRelations();

            var result = users.Select(u => new GetAppUserWithAllQueryResult
            {
                AppUserId = u.AppUserId,
                Name = u.Name,
                Surname = u.Surname,
                Email = u.Email,
                Password = u.Password,

                DepartmentId = u.DepartmentId,
                DepartmentName = u.Department.Name,

                AppRoleId = u.AppRoleId,
                AppRoleName = u.AppRole.AppRoleName

            }).ToList();

            return await Task.FromResult(result);
        }
    }
}
