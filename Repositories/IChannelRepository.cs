using Youtube.Api.Models; 

namespace Youtube.Api.Repositories
{
  public interface IChannelRepository
  {
    IEnumerable<Channel> GetAll();
    void Add(Channel channel);
    Channel? GetById(string id); 
  }
}