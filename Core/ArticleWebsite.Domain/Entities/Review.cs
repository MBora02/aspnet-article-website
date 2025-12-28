using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Domain.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; } // 1–5 arası
        public DateTime CreatedAt { get; set; }
        public string? Comment { get; set; }

        public int ReviewerId { get; set; }
        public AppUser Reviewer { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
