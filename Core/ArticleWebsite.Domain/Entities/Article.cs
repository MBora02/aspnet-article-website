using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Domain.Entities
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? PdfFilePath { get; set; }
        public string? ImagePath { get; set; }

        public int AuthorId { get; set; }
        public AppUser Author { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int StatusId { get; set; }
        public ArticleStatus Status { get; set; }

        public int TagCloudId { get; set; }  
        public TagCloud TagCloud { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
