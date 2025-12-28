using ArticleWebsite.Application.Features.Mediator.Queries.FaqQueries;
using ArticleWebsite.Application.Features.Mediator.Results.FaqResults;
using ArticleWebsite.Application.Interfaces;
using ArticleWebsite.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Handlers.FaqHandlers
{
    internal class GetFaqByIdQueryHandler : IRequestHandler<GetFaqByIdQuery, GetFaqByIdQueryResult>
    {
        private readonly IRepository<Faq> _repository;
        public GetFaqByIdQueryHandler(IRepository<Faq> repository)
        {
            _repository = repository;
        }
        public async Task<GetFaqByIdQueryResult> Handle(GetFaqByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.Id);
            return new GetFaqByIdQueryResult
            {
                FaqId=value.FaqId,
                Question=value.Question,
                Answer=value.Answer
            };
        }
    }
}
