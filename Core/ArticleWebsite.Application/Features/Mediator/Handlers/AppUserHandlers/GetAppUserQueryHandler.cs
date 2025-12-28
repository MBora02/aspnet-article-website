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
    public class GetAppUserQueryHandler : IRequestHandler<GetAppUserQuery, List<GetAppUserQueryResult>>
    {
        private readonly IRepository<AppUser> _repository;

        public GetAppUserQueryHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAppUserQueryResult>> Handle(GetAppUserQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetAppUserQueryResult
            {
                AppUserId=x.AppUserId,
                Name = x.Name,
                Surname=x.Surname, 
                Email=x.Email,
                Password=x.Password,
                DepartmentId=x.DepartmentId,
                AppRoleId= x.AppRoleId
            }).ToList();
        }
    }
}
