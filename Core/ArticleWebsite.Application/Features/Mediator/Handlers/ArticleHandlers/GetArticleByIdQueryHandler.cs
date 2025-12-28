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
    public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, GetArticleByIdQueryResult>
    {
        private readonly IArticleRepository _repository;

        public GetArticleByIdQueryHandler(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetArticleByIdQueryResult> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetArticleByIdAsync(request.Id);
            return new GetArticleByIdQueryResult
            {
                AuthorId = values.AuthorId,
                AuthorName=values.Author.Name,
                ArticleId = values.ArticleId,
                DepartmentName=values.Department.Name,
                DepartmentId = values.DepartmentId,
                Content = values.Content,
                UpdatedAt = values.UpdatedAt,
                ImagePath = values.ImagePath,
                CreatedAt = values.CreatedAt,
                Title = values.Title,
                StatusId = values.StatusId,
                TagCloudId =values.TagCloudId,
                TagCloudTitle=values.TagCloud.Title
            };
        }
    }
}
