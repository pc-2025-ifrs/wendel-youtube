using Microsoft.AspNetCore.Mvc;
using Youtube.Api.Repositories;
using Youtube.Api.Models; 

namespace Youtube.Api.Controllers
{
  [ApiController]
  [Route("api/channels")]
  public class ChannelsController : ControllerBase
  {
    private readonly IChannelRepository _repository;

    public ChannelsController(IChannelRepository repository)
    {
      _repository = repository;
    }

    // GET /api/channels
    [HttpGet]
    public IEnumerable<Channel> GetAll()
    {
      return _repository.GetAll();
    }
    
    public record CreateChannelParams(string Name, string Email, string Password, string Image);

    // POST /api/channels
    [HttpPost]
    public IActionResult Create([FromBody] CreateChannelParams input)
    {
      var channel = new Channel(input.Name, input.Email, input.Password, input.Image);
      
      _repository.Add(channel);

      return CreatedAtAction(nameof(GetAll), new { id = channel.id }, channel);
    }
  }
}