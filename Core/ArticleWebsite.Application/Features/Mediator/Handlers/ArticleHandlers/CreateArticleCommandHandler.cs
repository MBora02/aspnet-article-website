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
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand>
    {
        private readonly IRepository<Article> _articleRepository;
        private readonly IFileService _fileService;

        public CreateArticleCommandHandler(IRepository<Article> articleRepository, IFileService fileService)
        {
            _articleRepository = articleRepository;
            _fileService = fileService;
        }
        public async Task Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var pdfFilePath = request.PdfFile != null
    ? await _fileService.SaveFileAsync(request.PdfFile, "uploads/pdfs")
    : null;

            var imagePath = request.ImageFile != null
                ? await _fileService.SaveFileAsync(request.ImageFile, "uploads/images")
                : null;
            try
            {
                var article = new Article
                {
                    Title = request.Title,
                    Content = request.Content,
                    PdfFilePath = pdfFilePath,
                    ImagePath =imagePath,
                    AuthorId = request.AuthorId,
                    DepartmentId = request.DepartmentId,
                    StatusId = 1,
                    TagCloudId = request.TagCloudId,
                    CreatedAt = DateTime.UtcNow
                };

                await _articleRepository.CreateAsync(article);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Makale kaydı sırasında hata: {ex.Message}");
                throw;
            }
        }
    }
}
