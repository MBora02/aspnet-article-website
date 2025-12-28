using ArticleWebsite.Application.Features.Mediator.Commands.ArticleCommands;
using ArticleWebsite.Application.Features.Mediator.Queries.ArticleQueries;
using ArticleWebsite.Application.Interfaces;
using ArticleWebsite.Dto.ArticleDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticleWebsite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFileService _fileService;

        public ArticlesController(IMediator mediator, IFileService fileService)
        {
            _mediator = mediator;
            _fileService = fileService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ArticleList()
        {
            var values = await _mediator.Send(new GetArticleQuery());
            return Ok(values);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(int id)
        {
            var value = await _mediator.Send(new GetArticleByIdQuery(id));
            return Ok(value);
        }

        [AllowAnonymous]
        [HttpGet("with-all")]
        public async Task<IActionResult> GetArticlesWithAll()
        {
            var values = await _mediator.Send(new GetArticleWithAllQuery());
            return Ok(values);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromForm] CreateArticleCommand command)
        {
            
            await _mediator.Send(command);
            return Ok("Makale başarıyla eklendi");
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> RemoveArticle(int id)
        {
            await _mediator.Send(new RemoveArticleCommand(id));
            return Ok("Makale başarıyla silindi");
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(UpdateArticleCommand command)
        {
            await _mediator.Send(command);
            return Ok("Makale başarıyla güncellendi");
        }

        [AllowAnonymous]
        [HttpGet("GetLast3ArticlesWitAuthorsList")]
        public async Task<IActionResult> GetLast3ArticlesWitAuthorsList()
        {
            var values = await _mediator.Send(new GetLast3ArticlesWithAuthorsQuery());
            return Ok(values);
        }

        [AllowAnonymous]
        [HttpGet("GetArticleByAuthorId")]
        public async Task<IActionResult> GetArticleByAuthorId(int id)
        {
            var values = await _mediator.Send(new GetArticleByAuthorIdQuery(id));
            return Ok(values);
        }

        [AllowAnonymous]
        [HttpGet("GetArticleWithAuthorId")]
        public async Task<IActionResult> GetArticleWithAuthorId(int id)
        {
            var values = await _mediator.Send(new GetArticleWithAuthorIdQuery(id));
            return Ok(values);
        }
        [AllowAnonymous]
        [HttpGet("GetArticleByDepartmentId")]
        public async Task<IActionResult> GetArticleByDepartmentId(int id)
        {
            var values = await _mediator.Send(new GetArticleByDepartmentIdQuery(id));
            return Ok(values);
        }

        [AllowAnonymous]
        [HttpGet("GetArticleByTagCloudId")]
        public async Task<IActionResult> GetArticleByTagCloudId(int id)
        {
            var values = await _mediator.Send(new GetArticleByTagCloudIdQuery(id));
            return Ok(values);
        }

        [Authorize]
        [HttpPut("SendForApproval/{id}")]
        public async Task<IActionResult> SendForApproval(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int authorId))
            {
                return Unauthorized("Kullanıcı doğrulanamadı.");
            }

            var command = new SendArticleForApprovalCommand
            {
                ArticleId = id,
                AuthorId = authorId
            };

            try
            {
                await _mediator.Send(command);
                return Ok("Makale onay için gönderildi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Instructor")]
        [HttpPut("Approve/{id}")]
        public async Task<IActionResult> ApproveArticle(int id)
        {
            await _mediator.Send(new ApproveArticleCommand(id));
            return Ok("Makale onaylandı.");
        }

        [Authorize(Roles = "Instructor")]
        [HttpPut("Reject/{id}")]
        public async Task<IActionResult> RejectArticle(int id)
        {
            await _mediator.Send(new RejectArticleCommand(id));
            return Ok("Makale reddedildi.");
        }

        [AllowAnonymous]
        [HttpGet("pending-by-department")]
        public async Task<IActionResult> GetPendingArticlesByDepartment()
        {
            var values = await _mediator.Send(new GetPendingArticlesByDepartmentAsyncQuery());
            return Ok(values);
        }
    }
}
