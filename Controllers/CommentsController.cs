using Microsoft.AspNetCore.Mvc;
using Youtube.Api.Repositories;
using Youtube.Api.Models;

namespace Youtube.Api.Controllers
{
  [ApiController]
  [Route("api/comments")]
  public class CommentsController : ControllerBase
  {
    private readonly ICommentRepository _commentRepository;
    private readonly IVideoRepository _videoRepository;
    private readonly IChannelRepository _channelRepository;
    

    public CommentsController(ICommentRepository commentRepository, 
                              IVideoRepository videoRepository, 
                              IChannelRepository channelRepository)
    {
      _commentRepository = commentRepository;
      _videoRepository = videoRepository;
      _channelRepository = channelRepository;
    }

    // GET /api/comments
    [HttpGet]
    public IEnumerable<Comment> GetAll()
    {
      return _commentRepository.GetAll();
    }
    public record CreateCommentParams(string Content, string VideoId, string ChannelId);

        // POST /api/comments
        [HttpPost]
        public IActionResult Create([FromBody] CreateCommentParams input)
        {
            var video = _videoRepository.GetById(input.VideoId);
            var channel = _channelRepository.GetById(input.ChannelId);

            if (video == null || channel == null)
            {
                return BadRequest("VideoId ou ChannelId inválido.");
            }

            var comment = new Comment(input.Content, video, channel);

            _commentRepository.Add(comment);

            return CreatedAtAction(nameof(GetAll), comment);
        }
    
    public record CreateReplyParams(string Content, string ChannelId);

    // POST /api/comments/{parentCommentId}/reply
    [HttpPost("{parentCommentId}/reply")]
    public IActionResult CreateReply(string parentCommentId, [FromBody] CreateReplyParams input)
    {
      var parentComment = _commentRepository.GetById(parentCommentId);
      if (parentComment == null)
      {
        return NotFound("O comentário principal (pai) não foi encontrado.");
      }

      var replyingChannel = _channelRepository.GetById(input.ChannelId);
      if (replyingChannel == null)
      {
        return BadRequest("O ChannelId do autor da resposta é inválido.");
      }

      var newReply = new Comment(input.Content, parentComment.Video, replyingChannel);

      parentComment.AddComment(newReply);

      return CreatedAtAction(nameof(GetAll), new { id = newReply.Id }, newReply);
    }
  }
}