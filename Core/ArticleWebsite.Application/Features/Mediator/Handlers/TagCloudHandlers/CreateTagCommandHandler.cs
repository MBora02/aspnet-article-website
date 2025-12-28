using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Features.Mediator.Commands.TagCloudCommands;
using ArticleWebsite.Application.Interfaces;
using ArticleWebsite.Domain.Entities;
using MediatR;

namespace ArticleWebsite.Application.Features.Mediator.Handlers.TagCloudHandlers
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCloudCommand>
    {
        private readonly IRepository<TagCloud> _repository;
        public CreateTagCommandHandler(IRepository<TagCloud> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateTagCloudCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new TagCloud
            {
                Title = request.Title
            });
        }
    }
}
