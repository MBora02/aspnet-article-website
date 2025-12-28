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
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, GetUserProfileQueryResult>
    {
        private readonly IAppUserRepository _appUserRepository;

        public GetUserProfileQueryHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }
        public async Task<GetUserProfileQueryResult> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _appUserRepository.GetUserProfileAsync(request.UserId);

            if (user == null)
                return null;

            return new GetUserProfileQueryResult
            {
                AppUserId = user.AppUserId,
                DepartmentId=user.DepartmentId,
                AppRoleId=user.AppRoleId,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                DepartmentName = user.Department?.Name,
                AppRoleName = user.AppRole?.AppRoleName
            };
        }
    }
}
