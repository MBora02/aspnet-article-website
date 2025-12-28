using ArticleWebsite.Application.Features.Mediator.Commands.ArticleCommands;
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
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand>
    {
        private readonly IRepository<Article> _articleRepository;
        private readonly IFileService _fileService;

        public UpdateArticleCommandHandler(IRepository<Article> articleRepository, IFileService fileService)
        {
            _articleRepository = articleRepository;
            _fileService = fileService;
        }
        public async Task Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetByIdAsync(request.ArticleId);
            if (article == null)
                throw new Exception($"Makale bulunamadı (Id={request.ArticleId})");

            article.Title = request.Title;
            article.Content = request.Content;
            article.AuthorId = request.AuthorId;
            article.DepartmentId = request.DepartmentId;
            article.StatusId = request.StatusId;
            article.TagCloudId = request.TagCloudId;
            article.UpdatedAt = DateTime.UtcNow;

            // Eğer yeni PDF yüklendiyse güncelle
            if (request.PdfFilePath != null)
            {
                var pdfFilePath = await _fileService.SaveFileAsync(request.PdfFilePath, "uploads/pdfs");
                article.PdfFilePath = pdfFilePath;
            }

            if (request.ImagePath != null)
            {
                var imagePath = await _fileService.SaveFileAsync(request.ImagePath, "uploads/images");
                article.ImagePath = imagePath;
            }

            await _articleRepository.UpdateAsync(article);
        }
    }
}
