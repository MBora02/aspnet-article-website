using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace ArticleWebsite.Dto.ArticleDtos
{
    public class CreateArticleDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile? PdfFile { get; set; }
        public IFormFile? ImageFile { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

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
