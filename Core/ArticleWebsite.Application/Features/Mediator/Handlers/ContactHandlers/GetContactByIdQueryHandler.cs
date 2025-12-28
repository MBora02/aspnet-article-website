using ArticleWebsite.Application.Features.Mediator.Queries.ContactQueries;
using ArticleWebsite.Application.Features.Mediator.Results.ContactResults;
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
    public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, GetContactByIdQueryResult>
    {
        private readonly IRepository<Contact> _repository;
        public GetContactByIdQueryHandler(IRepository<Contact> repository)
        {
            _repository = repository;
        }
        public async Task<GetContactByIdQueryResult> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.Id);
            return new GetContactByIdQueryResult
            {
                ContactID= value.ContactID,
                Name=value.Name,
                Surname=value.Surname,
                Email=value.Email,
                Subject=value.Subject,
                Message=value.Message,
                SendDate=value.SendDate
            };
        }
    }
}
