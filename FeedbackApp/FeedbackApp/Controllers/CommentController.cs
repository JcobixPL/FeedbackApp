using FeedbackApp.Entities;
using FeedbackApp.Models.Comments;
using FeedbackApp.Repositories.Comments;
using FeedbackApp.Repositories.Feedbacks;
using FeedbackApp.Repositories.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FeedbackApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFeedbackRepository _feedbackRepository;

        public CommentController(ICommentRepository commentRepository, IUserRepository userRepository, IFeedbackRepository feedbackRepository)
        {
            _commentRepository = commentRepository;
            _feedbackRepository = feedbackRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();

            var commentDtos = comments.Select(comment => new CommentDto
            {
                Id = comment.Id,
                FeedbackId = comment.FeedbackId,
                UserId = comment.UserId,
                CreatedAt = comment.CreatedAt,
                Text = comment.Text
            }).ToList();

            return Ok(commentDtos);
        }

        [HttpGet("by-feedback-id/{feedbackId}")]
        public async Task<IActionResult> GetCommentsByFeedbackId(int feedbackId)
        {
            var comment = await _commentRepository.GetCommentsByFeedbackIdAsync(feedbackId);
            if (comment is null)
            {
                return NotFound();
            }

            var commentDetail = comment.Select(comment => new CommentDto
            {
                Id = comment.Id,
                FeedbackId = comment.FeedbackId,
                UserId = comment.UserId,
                CreatedAt = comment.CreatedAt,
                Text = comment.Text
            }).ToList();

            return Ok(commentDetail);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment is null)
            {
                return NotFound();
            }

            var commentDetail = new CommentDto
            {
                Id = comment.Id,
                FeedbackId = comment.FeedbackId,
                UserId = comment.UserId,
                CreatedAt = comment.CreatedAt,
                Text = comment.Text
            };

            return Ok(commentDetail);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CreateCommentDto createCommentDto)
        {
            if (createCommentDto is null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetUserByIdAsync(int.Parse(userId));

            var feedback = await _feedbackRepository.GetFeedbackByIdAsync(createCommentDto.FeedbackId);

            var comment = new Comment
            {
                UserId = int.Parse(userId),
                FeedbackId = createCommentDto.FeedbackId,
                Text = createCommentDto.Text,
                CreatedAt = DateTime.UtcNow,
                User = user,
                Feedback = feedback
            };

            await _commentRepository.AddCommentAsync(comment);

            var commentDto = new CommentDto
            {
                Id = comment.Id,
                UserId = comment.UserId,
                FeedbackId = comment.FeedbackId,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt
            };

            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, commentDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment is null)
            {
                return NotFound();
            }

            comment.Text = updateCommentDto.Text;

            await _commentRepository.UpdateCommentAsync(comment);
            return Ok(updateCommentDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            await _commentRepository.DeleteCommentAsync(id);
            return NoContent();
        }
    }
}
