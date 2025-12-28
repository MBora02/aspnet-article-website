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
    public class GetArticleWithAllQueryHandler : IRequestHandler<GetArticleWithAllQuery, List<GetArticleWithAllQueryResult>>
    {
        private readonly IArticleRepository _repository;

        public GetArticleWithAllQueryHandler(IArticleRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<GetArticleWithAllQueryResult>> Handle(GetArticleWithAllQuery request, CancellationToken cancellationToken)
        {
            var articles =await _repository.GetArticleWithAllAsync();

            var result = articles.Select(a => new GetArticleWithAllQueryResult
            {
                ArticleId = a.ArticleId,
                Title = a.Title,
                Content = a.Content,
                PdfFilePath = a.PdfFilePath,
                ImagePath = a.ImagePath,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt,

                AuthorId = a.AuthorId,
                AuthorEmail = a.Author.Email,
                AuthorName = a.Author.Name,
                AuthorSurname = a.Author.Surname,

                DepartmentId = a.DepartmentId,
                DepartmentName = a.Department.Name,

                StatusId = a.StatusId,
                StatusName = a.Status.Name,

                TagCloudId = a.TagCloudId,
                TagCloudTitle = a.TagCloud.Title

            }).ToList();

            return result;
        }
    }
}
