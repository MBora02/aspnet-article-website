using ArticleWebsite.Application.Features.Mediator.Results.AppUserResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Queries.AppUserQueries
{
    public class GetUserProfileQuery : IRequest<GetUserProfileQueryResult>
    {
        public int UserId { get; set; }

        public GetUserProfileQuery(int userId)
        {
            UserId = userId;
        }
    }
}
