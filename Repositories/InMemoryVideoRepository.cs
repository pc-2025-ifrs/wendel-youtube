using Youtube.Api.Models;

namespace Youtube.Api.Repositories
{
  public class InMemoryVideoRepository : IVideoRepository
  {
    private static readonly List<Video> _videos = [];

    public void Add(Video video)
    {
      _videos.Add(video);
    }

    public IEnumerable<Video> GetAll()
    {
      return _videos;
    }

    public Video? GetById(string id)
    {
        return _videos.FirstOrDefault(v => v.Id == id);
    }
  }
}