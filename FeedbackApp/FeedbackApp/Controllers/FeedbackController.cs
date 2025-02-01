using FeedbackApp.Entities;
using FeedbackApp.Models.Comments;
using FeedbackApp.Models.Feedbacks;
using FeedbackApp.Repositories.Feedbacks;
using FeedbackApp.Repositories.Projects;
using FeedbackApp.Repositories.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FeedbackApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;


        public FeedbackController(IFeedbackRepository feedbackRepository, IUserRepository userRepository, IProjectRepository projectRepository )
        {
            _feedbackRepository = feedbackRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            var feedbacks = await _feedbackRepository.GetAllFeedbacksAsync();

            var feedbackDtos = feedbacks.Select(feedback => new FeedbackDto
            {
                Id = feedback.Id,
                Type = feedback.Type,
                Priority = feedback.Priority,
                Status = feedback.Status,
                CreatedAt = feedback.CreatedAt,
                Description = feedback.Description,
                UserId = feedback.UserId,
                ProjectId = feedback.ProjectId,
                Comments = feedback.Comments.Select(comment => new CommentDto
                {
                    Id = comment.Id,
                    FeedbackId = comment.FeedbackId,
                    UserId = comment.UserId,
                    CreatedAt = comment.CreatedAt,
                    Text = comment.Text
                }).ToList()
            }).ToList();

            return Ok(feedbackDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedbackById(int id)
        {
            var feedback = await _feedbackRepository.GetFeedbackByIdAsync(id);
            if (feedback is null)
            {
                return NotFound();
            }

            var feedbackDetail = new FeedbackDto
            {
                Id = feedback.Id,
                Type = feedback.Type,
                Priority = feedback.Priority,
                Status = feedback.Status,
                CreatedAt = feedback.CreatedAt,
                Description = feedback.Description,
                UserId = feedback.UserId,
                ProjectId = feedback.ProjectId,
                Comments = feedback.Comments.Select(comment => new CommentDto
                {
                    Id = comment.Id,
                    FeedbackId = comment.FeedbackId,
                    UserId = comment.UserId,
                    CreatedAt = comment.CreatedAt,
                    Text = comment.Text
                }).ToList()
            };

            return Ok(feedbackDetail);
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedback(
            [FromQuery] FeedbackType type, 
            [FromQuery] FeedbackPriority priority, 
            [FromBody] CreateFeedbackDto createFeedbackDto)
        {
            if (createFeedbackDto is null)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetUserByIdAsync(int.Parse(userId));

            var project = await _projectRepository.GetProjectByIdAsync(createFeedbackDto.ProjectId);

            var feedback = new Feedback
            {
                Description = createFeedbackDto.Description,
                Type = type,
                Status = FeedbackStatus.New,
                Priority = priority,
                UserId = int.Parse(userId),
                CreatedAt = DateTime.UtcNow,
                ProjectId = createFeedbackDto.ProjectId,
                User = user,
                Project = project
            };

            await _feedbackRepository.AddFeedbackAsync(feedback);

            var feedbackDto = new FeedbackDto
            {
                Id = feedback.Id,
                Description = feedback.Description,
                Type = feedback.Type,
                Status = feedback.Status,
                Priority = feedback.Priority,
                UserId = feedback.UserId,
                CreatedAt = feedback.CreatedAt,
                ProjectId = feedback.ProjectId
            };

            return CreatedAtAction(nameof(GetFeedbackById), new { id = feedback.Id }, feedbackDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedback(
    int id,
    [FromQuery] FeedbackType? type,
    [FromQuery] FeedbackStatus? status,
    [FromQuery] FeedbackPriority? priority,
    [FromBody] UpdateFeedbackDto updateFeedbackDto)
        {
            try
            {
                if (updateFeedbackDto is null)
                {
                    return BadRequest("Feedback data is required.");
                }

                var feedback = await _feedbackRepository.GetFeedbackByIdAsync(id);
                if (feedback is null)
                {
                    return NotFound("Feedback not found.");
                }
                if (type.HasValue)
                {
                    feedback.Type = type.Value;
                }

                if (priority.HasValue)
                {
                    feedback.Priority = priority.Value;
                }

                if (type.HasValue)
                {
                    feedback.Status = status.Value;
                }

                feedback.Description = updateFeedbackDto.Description;

                await _feedbackRepository.UpdateFeedbackAsync(feedback);

                var feedbackDto = new FeedbackDto
                {
                    Id = feedback.Id,
                    Description = feedback.Description,
                    Type = feedback.Type,
                    Status = feedback.Status,
                    Priority = feedback.Priority,
                    UserId = feedback.UserId,
                    CreatedAt = feedback.CreatedAt,
                    ProjectId = feedback.ProjectId
                };

                return Ok(feedbackDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating feedback.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            await _feedbackRepository.DeleteFeedbackAsync(id);
            return NoContent();
        }
    }
}
