using ArticleWebsite.Application.Features.Mediator.Commands.FaqCommands;
using ArticleWebsite.Application.Features.Mediator.Queries.FaqQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticleWebsite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FaqsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> FaqList()
        {
            var values = await _mediator.Send(new GetFaqQuery());
            return Ok(values);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateFaq(CreateFaqCommand command)
        {
            await _mediator.Send(command);
            return Ok("Sık Sorulan Sorular Bilgisi Eklendi");
        }
        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFaq(int id)
        {
            var values = await _mediator.Send(new GetFaqByIdQuery(id));
            return Ok(values);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> RemoveFaq(int id)
        {
            await _mediator.Send(new RemoveFaqCommand(id));
            return Ok("Sık Sorulan Sorular Bilgisi Silindi");
        }
        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> UpdateFaq(UpdateFaqCommand command)
        {
            await _mediator.Send(command);
            return Ok("Sık Sorulan Sorular Bilgisi Güncellendi");
        }
    }
}