using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ArticleWebsite.Application.Features.Mediator.Commands.ArticleCommands
{
    public class UpdateArticleCommand:IRequest
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile? PdfFilePath { get; set; }
        public IFormFile? ImagePath { get; set; }
        public int AuthorId { get; set; }
        public int DepartmentId { get; set; }
        public int StatusId { get; set; }
        public int TagCloudId { get; set; }
    }
}
