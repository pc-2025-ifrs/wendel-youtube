

namespace Youtube.Api.Models
{
  public record class Channel
  {
    public string id { get; init; }
    public string Email{ get; init; }
    public string Password{ get; init; }
    public string Name{ get; init; }
    public string Image { get; set; }
    public IList<Video> Videos { get; init; }

    private IList<Channel> _subscribers = [];

    public int NumberSubscribers => _subscribers.Count;

    public Channel(string name, string email, string password, string image)
    {
      id = Utils.RandomId();
      Name = name;
      Email = email;
      Image = image;
      Password = cryptography(password);
      Videos = [];
    }

    private string cryptography(string password)
    {
      return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public void AddVideo(Video video)
    {
      Videos.Add(video);
    }

    public bool VerifyPassword(string password)
    {
      return BCrypt.Net.BCrypt.Verify(password, Password);
    }

    public void AddSubscriber(Channel channel)
    {
      _subscribers.Add(channel);
    }

  }

}