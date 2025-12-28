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
    public class GetLast3ArticlesWithAuthorsQueryHandler : IRequestHandler<GetLast3ArticlesWithAuthorsQuery, List<GetLast3ArticlesWithAuthorsQueryResult>>
    {
        private readonly IArticleRepository _repository;

        public GetLast3ArticlesWithAuthorsQueryHandler(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetLast3ArticlesWithAuthorsQueryResult>> Handle(GetLast3ArticlesWithAuthorsQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetLast3ArticleWithAuthorsAsync();
            return values.Select(x => new GetLast3ArticlesWithAuthorsQueryResult
            {
                AuthorId = x.AuthorId,
                ArticleId = x.ArticleId,
                DepartmentId = x.DepartmentId,
                Content = x.Content,
                UpdatedAt = x.UpdatedAt,
                ImagePath = x.ImagePath,
                CreatedAt = x.CreatedAt,
                Title = x.Title,
                AuthorName = x.Author.Name,
                AuthorSurname=x.Author.Surname,
                DepartmentName=x.Department.Name,
                StatusId = 1
            }).ToList();
        }
    }
}
