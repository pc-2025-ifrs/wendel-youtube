using Youtube.Api.Models; 

namespace Youtube.Api.Repositories
{
  public class InMemoryChannelRepository : IChannelRepository
  {
    private static readonly List<Channel> _channels = [];

    public void Add(Channel channel)
    {
      _channels.Add(channel);
    }

    public IEnumerable<Channel> GetAll()
    {
      return _channels;
    }

    public Channel? GetById(string id)
    {
        return _channels.FirstOrDefault(c => c.id == id);
    }
  }
}