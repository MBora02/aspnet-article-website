using ArticleWebsite.Application.Features.Mediator.Queries.ArticleQueries;
using ArticleWebsite.Application.Features.Mediator.Results.ArticleResults;
using ArticleWebsite.Application.Interfaces;
using ArticleWebsite.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Handlers.ArticleHandlers
{
    public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, List<GetArticleQueryResult>>
    {
        private readonly IRepository<Article> _repository;

        public GetArticleQueryHandler(IRepository<Article> repository)
        {
            _repository = repository;
        }
        public async Task<List<GetArticleQueryResult>> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            var articles = await _repository.GetAllAsync();

            
            return articles.Select(a => new GetArticleQueryResult
            {
                ArticleId = a.ArticleId,
                Title = a.Title,
                Content = a.Content,
                PdfFilePath = a.PdfFilePath,
                ImagePath = a.ImagePath,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt,
                AuthorId = a.AuthorId,
                DepartmentId = a.DepartmentId,
                StatusId = a.StatusId,
                TagCloudId = a.TagCloudId,
            }).ToList();
        }
    }
}
