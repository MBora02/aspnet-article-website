using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Commands.ReviewCommands
{
    public class UpdateReviewCommand : IRequest
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; } // 1–5 arası
        public string? Comment { get; set; }
        public int ReviewerId { get; set; }

        public int ArticleId { get; set; }
    }
}
