using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Results.ArticleResults
{
    public class GetArticleByTagCloudIdQueryResult
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? PdfFilePath { get; set; }
        public string? ImagePath { get; set; }

        public int AuthorId { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public int TagCloudId { get; set; }
        public string TagCloudTitle { get; set; }
    }
}
