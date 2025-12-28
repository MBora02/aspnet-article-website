using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Features.Mediator.Queries.ArticleQueries;
using ArticleWebsite.Application.Features.Mediator.Results.ArticleResults;
using ArticleWebsite.Application.Interfaces;
using ArticleWebsite.Domain.Entities;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Handlers.ArticleHandlers
{
    public class GetArticleWithAuthorIdQueryHandler : IRequestHandler<GetArticleWithAuthorIdQuery, List<GetArticleWithAuthorIdQueryResult>>
    {
        private readonly IArticleRepository _repository;

        public GetArticleWithAuthorIdQueryHandler(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetArticleWithAuthorIdQueryResult>> Handle(GetArticleWithAuthorIdQuery request, CancellationToken cancellationToken)
        {
            var values = _repository.GetArticleWithAuthorId(request.AuthorId);
            return values.Select(x => new GetArticleWithAuthorIdQueryResult
            {
                DepartmentId = x.DepartmentId,
                AuthorId = x.AuthorId,
                ArticleId = x.ArticleId,

                AuthorEmail = x.Author?.Email,
                AuthorName = x.Author?.Name,
                AuthorSurname = x.Author?.Surname,

                DepartmentName = x.Department?.Name,

                TagCloudId = x.TagCloudId,
                TagCloudTitle = x.TagCloud?.Title
            }).ToList();
        }
    }
}
