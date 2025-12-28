using ArticleWebsite.Application.Features.Mediator.Queries.AppRoleQueries;
using ArticleWebsite.Application.Features.Mediator.Results.AppRoleResults;
using ArticleWebsite.Application.Features.Mediator.Results.TagCloudResults;
using ArticleWebsite.Application.Interfaces;
using ArticleWebsite.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Handlers.AppRoleHandlers
{
    public class GetAppRoleQueryHandler : IRequestHandler<GetAppRoleQuery, List<GetAppRoleQueryResult>>
    {
        private readonly IRepository<AppRole> _repository;
        public GetAppRoleQueryHandler(IRepository<AppRole> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAppRoleQueryResult>> Handle(GetAppRoleQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetAppRoleQueryResult
            {
                AppRoleId = x.AppRoleId,
                AppRoleName = x.AppRoleName
            }).ToList();
        }
    }
}
