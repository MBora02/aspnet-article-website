using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Commands.DepartmentCommands
{
    public class CreateDepartmentCommand:IRequest
    {
        public string Name { get; set; }
    }
}
