using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Commands.FaqCommands
{
    public class RemoveFaqCommand:IRequest
    {
        public int Id { get; set; }

        public RemoveFaqCommand(int id)
        {
            Id = id;
        }
    }
}
