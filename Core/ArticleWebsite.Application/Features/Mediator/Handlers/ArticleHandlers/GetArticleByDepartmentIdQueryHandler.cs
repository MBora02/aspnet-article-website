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
    public class GetArticleByDepartmentIdQueryHandler : IRequestHandler<GetArticleByDepartmentIdQuery, List<GetArticleByDepartmentIdQueryResult>>
    {
        private readonly IArticleRepository _repository;

        public GetArticleByDepartmentIdQueryHandler(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetArticleByDepartmentIdQueryResult>> Handle(GetArticleByDepartmentIdQuery request, CancellationToken cancellationToken)
        {
            var values = _repository.GetArticleByDepartmentId(request.Id);
            return values.Select(x => new GetArticleByDepartmentIdQueryResult
            {
                DepartmentId = x.DepartmentId,
                AuthorId = x.AuthorId,
                ArticleId = x.ArticleId,
                Content = x.Content,
                UpdatedAt = x.UpdatedAt,
                ImagePath = x.ImagePath,
                CreatedAt = x.CreatedAt,
                Title = x.Title,
                PdfFilePath = x.PdfFilePath,
                
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
