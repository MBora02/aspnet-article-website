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
    public class SendArticleForApprovalCommandHandler : IRequestHandler<SendArticleForApprovalCommand>
    {
        private readonly IRepository<Article> _articleRepository;

        public SendArticleForApprovalCommandHandler(IRepository<Article> articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task Handle(SendArticleForApprovalCommand request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetByIdAsync(request.ArticleId);
            if (article == null || article.AuthorId != request.AuthorId)
            {
                throw new Exception("Makale bulunamadı veya yetkiniz yok.");
            }
            article.StatusId = 2; // Danışman onayı bekliyor
            article.UpdatedAt = DateTime.UtcNow;

            await _articleRepository.UpdateAsync(article);
        }
    }
}
