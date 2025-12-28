using ArticleWebsite.Application.Features.Mediator.Commands.ContactCommands;
using ArticleWebsite.Application.Interfaces;
using ArticleWebsite.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Handlers.ContactHandlers
{
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand>
    {
        private readonly IRepository<Contact> _repository;

        public UpdateContactCommandHandler(IRepository<Contact> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var values=await _repository.GetByIdAsync(request.ContactID);
            values.Name = request.Name;
            values.Surname=request.Surname;
            values.Email=request.Email;
            values.Subject=request.Subject;
            values.Message=request.Message;
            values.SendDate = DateTime.Now;
            await _repository.UpdateAsync(values);
        }
    }
}
