using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Commands.ArticleCommands
{
    public class ApproveArticleCommand:IRequest
    {
        public int ArticleId { get; set; }

        public ApproveArticleCommand(int articleId)
        {
            ArticleId = articleId;
        }
    }
}
