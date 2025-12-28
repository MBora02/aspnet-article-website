using ArticleWebsite.Application.Features.Mediator.Queries.AppRoleQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticleWebsite.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AppRoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppRoleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> AppRoleList()
        {
            var values = await _mediator.Send(new GetAppRoleQuery());
            return Ok(values);
        }
    }
}
