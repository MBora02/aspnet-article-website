using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Features.Mediator.Queries.ArticleQueries;
using ArticleWebsite.Application.Features.Mediator.Results.ArticleResults;
using ArticleWebsite.Application.Interfaces;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Handlers.ArticleHandlers
{
    public class GetPendingArticlesByDepartmentAsyncQueryHandler : IRequestHandler<GetPendingArticlesByDepartmentAsyncQuery, List<GetPendingArticlesByDepartmentAsyncQueryResult>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserAccessor _userAccessor;

        public GetPendingArticlesByDepartmentAsyncQueryHandler(IUserAccessor userAccessor, IArticleRepository articleRepository)
        {
            _userAccessor = userAccessor;
            _articleRepository = articleRepository;
        }

        public async Task<List<GetPendingArticlesByDepartmentAsyncQueryResult>> Handle(GetPendingArticlesByDepartmentAsyncQuery request, CancellationToken cancellationToken)
        {
            int departmentId = _userAccessor.GetUserDepartmentId();

            var articles = await _articleRepository.GetPendingArticlesByDepartmentAsync(departmentId);

            return articles.Select(a => new GetPendingArticlesByDepartmentAsyncQueryResult
            {
                ArticleId = a.ArticleId,
                Title = a.Title,
                Content = a.Content,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt,
                PdfFilePath = a.PdfFilePath,
                ImagePath = a.ImagePath,
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
        }
    }
}
