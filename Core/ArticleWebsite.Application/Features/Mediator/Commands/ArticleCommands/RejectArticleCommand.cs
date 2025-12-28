using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Commands.ArticleCommands
{
    public class RejectArticleCommand:IRequest
    {
        public int ArticleId { get; set; }

        public RejectArticleCommand(int articleId)
        {
            ArticleId = articleId;
        }
    }
}
