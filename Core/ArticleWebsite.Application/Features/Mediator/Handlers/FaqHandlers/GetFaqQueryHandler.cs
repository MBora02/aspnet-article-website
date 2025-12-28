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
    public class GetFaqQueryHandler : IRequestHandler<GetFaqQuery, List<GetFaqQueryResult>>
    {
        private readonly IRepository<Faq> _repository;

        public GetFaqQueryHandler(IRepository<Faq> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetFaqQueryResult>> Handle(GetFaqQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetFaqQueryResult
            {
                FaqId=x.FaqId,
                Question=x.Question,
                Answer=x.Answer
            }).ToList();
        }
    }
}
