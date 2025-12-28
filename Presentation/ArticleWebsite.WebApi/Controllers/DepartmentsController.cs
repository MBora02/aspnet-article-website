using ArticleWebsite.Application.Features.Mediator.Commands.DepartmentCommands;
using ArticleWebsite.Application.Features.Mediator.Queries.DepartmentQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticleWebsite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> DepartmentList()
        {
            var values = await _mediator.Send(new GetDepartmentQuery());
            return Ok(values);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var value=await _mediator.Send(new GetDepartmentByIdQuery(id));
            return Ok(value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentCommand command)
        {
            await _mediator.Send(command);
            return Ok("Bölüm başarıyla oluşturuldu");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> RemoveDepartment(int id)
        {
            await _mediator.Send(new RemoveDepartmentCommand(id));
            return Ok("Bölüm başarıyla silindi");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateDepartment(UpdateDepartmentCommand command)
        {
            await _mediator.Send(command);
            return Ok("Bölüm başarıyla güncellendi");
        }
    }
}
