using Microsoft.AspNetCore.Mvc;
using Youtube.Api.Repositories;
using Youtube.Api.Models;

namespace Youtube.Api.Controllers
{
  [ApiController]
  [Route("api/videos")]
  public class VideosController : ControllerBase
  {
        private readonly IVideoRepository _repository;
    
    private readonly IChannelRepository _channelRepository;

    public VideosController(IVideoRepository repository, IChannelRepository channelRepository)
    {
            _repository = repository;
      _channelRepository = channelRepository;
    }

    // GET /api/videos
    [HttpGet]
    public IEnumerable<Video> GetAll()
    {
      return _repository.GetAll();
    }

    public record CreateVideoParams(string Title, string Description, string Thumbnail, string ChannelId);

    // POST /api/videos
    [HttpPost]
    public IActionResult Create([FromBody] CreateVideoParams input)
    {
      var ownerChannel = _channelRepository.GetById(input.ChannelId);

      if (ownerChannel == null)
      {
        return BadRequest("O ChannelId fornecido é inválido.");
      }

      var video = new Video(input.Title, input.Description, input.Thumbnail, ownerChannel);
      
      _repository.Add(video);

      ownerChannel.AddVideo(video);

      return CreatedAtAction(nameof(GetAll), new { id = video.Id }, video);
    }
  }
}