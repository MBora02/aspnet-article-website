using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Commands.ArticleCommands
{
    public class SendArticleForApprovalCommand:IRequest
    {
        public int ArticleId { get; set; }
        public int AuthorId { get; set; }
    }
}
