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
    public class RejectArticleCommandHandler: IRequestHandler<RejectArticleCommand>
    {
        private readonly IRepository<Article> _articleRepository;

        public RejectArticleCommandHandler(IRepository<Article> articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task Handle(RejectArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetByIdAsync(request.ArticleId);
            if (article == null)
                throw new Exception("Makale bulunamadı.");

            article.StatusId = 4; // Örn: 4 = Reddedildi
            article.UpdatedAt = DateTime.UtcNow;

            await _articleRepository.UpdateAsync(article);
        }
    }
}
