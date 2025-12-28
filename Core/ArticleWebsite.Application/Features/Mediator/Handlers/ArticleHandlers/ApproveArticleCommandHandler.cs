using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Features.Mediator.Commands.ArticleCommands;
using ArticleWebsite.Application.Interfaces;
using ArticleWebsite.Domain.Entities;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Handlers.ArticleHandlers
{
    public class ApproveArticleCommandHandler : IRequestHandler<ApproveArticleCommand>
    {
        private readonly IRepository<Article> _articleRepository;

        public ApproveArticleCommandHandler(IRepository<Article> articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task Handle(ApproveArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetByIdAsync(request.ArticleId);
            if (article == null)
                throw new Exception("Makale bulunamadı.");

            article.StatusId = 3; // Örn: 3 = Onaylandı
            article.UpdatedAt = DateTime.UtcNow;

            await _articleRepository.UpdateAsync(article);
        }
    }
}
