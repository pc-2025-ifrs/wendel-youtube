using Youtube.Api.Models;

namespace Youtube.Api.Repositories
{
  public interface IVideoRepository
  {
    IEnumerable<Video> GetAll();
    void Add(Video video);
    Video? GetById(string id); 
  }
}