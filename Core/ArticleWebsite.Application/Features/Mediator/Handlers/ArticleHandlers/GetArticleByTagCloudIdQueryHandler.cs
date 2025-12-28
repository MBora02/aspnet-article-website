using ArticleWebsite.Application.Features.Mediator.Queries.ArticleQueries;
using ArticleWebsite.Application.Features.Mediator.Results.ArticleResults;
using ArticleWebsite.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Handlers.ArticleHandlers
{
    public class GetArticleByTagCloudIdQueryHandler : IRequestHandler<GetArticleByTagCloudIdQuery, List<GetArticleByTagCloudIdQueryResult>>
    {
        private readonly IArticleRepository _repository;

        public GetArticleByTagCloudIdQueryHandler(IArticleRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<GetArticleByTagCloudIdQueryResult>> Handle(GetArticleByTagCloudIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetArticleByTagCloudIdAsync(request.Id);
            return values.Select(x => new GetArticleByTagCloudIdQueryResult
            {
                AuthorId = x.AuthorId,
                ArticleId = x.ArticleId,
                Content = x.Content,
                UpdatedAt = x.UpdatedAt,
                ImagePath = x.ImagePath,
                CreatedAt = x.CreatedAt,
                Title = x.Title,
                PdfFilePath = x.PdfFilePath,
                DepartmentId = x.DepartmentId,
                AuthorEmail = x.Author?.Email,
                AuthorName = x.Author?.Name,
                AuthorSurname = x.Author?.Surname,
                DepartmentName = x.Department?.Name,

                StatusId = x.StatusId,
                StatusName = x.Status?.Name,

                TagCloudId = x.TagCloudId,
                TagCloudTitle = x.TagCloud?.Title
            }).ToList();
        }
    }
}
