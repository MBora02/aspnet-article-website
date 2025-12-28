using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Commands.ArticleCommands
{
    public class RemoveArticleCommand:IRequest
    {
        public int Id { get; set; }

        public RemoveArticleCommand(int id)
        {
            Id = id;
        }
    }
}
