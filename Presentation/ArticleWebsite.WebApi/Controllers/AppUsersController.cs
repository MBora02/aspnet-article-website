using ArticleWebsite.Application.Features.Mediator.Commands.AppUserCommands;
using ArticleWebsite.Application.Features.Mediator.Queries.AppUserQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticleWebsite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppUsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AppUserList()
        {
            var values = await _mediator.Send(new GetAppUserQuery());
            return Ok(values);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppUser(int id)
        {
            var value = await _mediator.Send(new GetAppUserByIdQuery(id));
            return Ok(value);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("with-all")]
        public async Task<IActionResult> GetAppUsersWithAll()
        {
            var values = await _mediator.Send(new GetAppUserWithAllQuery());
            return Ok(values);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> RemoveAppUser(int id)
        {
            await _mediator.Send(new RemoveAppUserCommand(id));
            return Ok("Kullanıcı başarıyla silindi");
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateAppUser(UpdateAppUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("Kullanıcı başarıyla güncellendi");
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile(int id)
        {
            var userProfile = await _mediator.Send(new GetUserProfileQuery(id));
            if (userProfile == null)
                return NotFound();

            return Ok(userProfile);
        }
    }
}
