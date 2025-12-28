using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Results.ReviewResults
{
    public class GetReviewByAppUserIdQueryResult
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; } // 1–5 arası
        public DateTime CreatedAt { get; set; }
        public string? Comment { get; set; }
        public int ReviewerId { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerSurname { get; set; }
        public int ArticleId { get; set; }
        public string ArticleTitle { get; set; }
    }
}
