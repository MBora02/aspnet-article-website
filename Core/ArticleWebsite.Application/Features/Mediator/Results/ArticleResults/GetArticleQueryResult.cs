using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Domain.Entities;

namespace ArticleWebsite.Application.Features.Mediator.Results.ArticleResults
{
    public class GetArticleQueryResult
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? PdfFilePath { get; set; }
        public string? ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int AuthorId { get; set; }

        public int DepartmentId { get; set; }

        public int StatusId { get; set; }

        public int TagCloudId { get; set; }
    }
}
