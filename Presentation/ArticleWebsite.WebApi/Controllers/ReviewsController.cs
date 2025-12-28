using ArticleWebsite.Application.Features.Mediator.Commands.ReviewCommands;
using ArticleWebsite.Application.Features.Mediator.Queries.ReviewQueries;
using ArticleWebsite.Application.Validators;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArticleWebsite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("article/{id}")]
        public async Task<IActionResult> GetReviewByArticleId(int id)
        {
            var values = await _mediator.Send(new GetReviewByArticleIdQuery(id));
            return Ok(values);
        }

        [Authorize]
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetReviewByAppUserId(int id)
        {
            var values = await _mediator.Send(new GetReviewByAppUserIdQuery(id));
            return Ok(values);
        }

        [Authorize(Roles = "Student, Instructor")]
        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewCommand command)
        {
            CreateReviewValidator validator = new CreateReviewValidator();
            var validationResult = validator.Validate(command);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            await _mediator.Send(command);
            return Ok("Ekleme işlemi gerçekleşti");
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateReview(UpdateReviewCommand command)
        {
            await _mediator.Send(command);
            return Ok("Güncelleme işlemi gerçekleşti");
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> RemoveReview(int id)
        {
            await _mediator.Send(new RemoveReviewCommand(id));
            return Ok("Değerlendirme başarıyla silindi");
        }

        
    }
}
