using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Features.Mediator.Queries.AppUserQueries;
using ArticleWebsite.Application.Features.Mediator.Results.AppUserResults;
using ArticleWebsite.Application.Interfaces;
using ArticleWebsite.Domain.Entities;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class GetAppUserByIdQueryHandler : IRequestHandler<GetAppUserByIdQuery, GetAppUserByIdQueryResult>
    {
        private readonly IRepository<AppUser> _repository;

        public GetAppUserByIdQueryHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public async Task<GetAppUserByIdQueryResult> Handle(GetAppUserByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            return new GetAppUserByIdQueryResult
            {
                AppUserId = values.AppUserId,
                Name = values.Name,
                Surname = values.Surname,
                Email = values.Email,
                Password = values.Password,
                DepartmentId = values.DepartmentId,
                AppRoleId = values.AppRoleId
            };
        }
    }
}
