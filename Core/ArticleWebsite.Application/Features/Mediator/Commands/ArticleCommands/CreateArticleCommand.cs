using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ArticleWebsite.Application.Features.Mediator.Commands.ArticleCommands
{
    public class CreateArticleCommand:IRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile? PdfFile { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int AuthorId { get; set; }
        public int DepartmentId { get; set; }
        public int StatusId { get; set; }
        public int TagCloudId { get; set; }

    }
}
