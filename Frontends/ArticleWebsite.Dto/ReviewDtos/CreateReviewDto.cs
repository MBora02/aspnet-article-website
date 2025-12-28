using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Dto.ReviewDtos
{
    public class CreateReviewDto
    {
        public int Rating { get; set; } // 1–5 arası
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? Comment { get; set; }

        public int ReviewerId { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerSurname { get; set; }
        public string ReviewerEmail { get; set; }

        public int ArticleId { get; set; }
    }
}
