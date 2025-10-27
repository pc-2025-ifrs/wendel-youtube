
namespace Youtube.Api.Models
{
  public record class Video
  {
    public string Id {get; set;}
    public string Title {get; set;}
    public string Description {get; set;}
    public string Thumbnail { get; set; }
    public uint Likes { get; set; }
    public uint Deslikes {get; set;}
    private DateTime Date { get; set; }
    public IList<Comment> Comments { get; set; }
    
    public Channel OwnerChannel { get; init; }

    public Video(string title, string description, string thumb, Channel owner)
    {
      Id = Utils.RandomId();
      Title = title;
      Description = description;
      Thumbnail = thumb;
      Likes = 0;
      Deslikes = 0;
      Comments = [];
      Date = DateTime.Now;
      OwnerChannel = owner;
    }

    public string GetTime()
    {
      TimeSpan timeSinceComment = DateTime.Now - Date;

      if (timeSinceComment.TotalSeconds < 60)
      {
        return $"{ (int)timeSinceComment.TotalSeconds} seconds ago";
      }
      else if (timeSinceComment.TotalMinutes < 60)
      {
        return $"{ (int)timeSinceComment.TotalMinutes} minutes ago";
      }
      else if (timeSinceComment.TotalHours < 24)
      {
        return $"{ (int)timeSinceComment.TotalHours} hours ago";
      }
      else if (timeSinceComment.TotalDays < 30)
      {
        return $"{ (int)timeSinceComment.TotalDays} days ago";
      }
      else if (timeSinceComment.TotalDays < 365)
      {
        int months = (int)(timeSinceComment.TotalDays / 30);
        return $"{months} months ago";
      }
      else
      {
        int years = (int)(timeSinceComment.TotalDays / 365);
        return $"{years} years ago";
      }
    }
    public void AddLike()
    {
      Likes++;
    }

    public void AddDeslike()
    {
      Deslikes++;
    }

    public void AddComment(string content, Channel channel)
    {
      Comments.Add(new Comment(content, this, channel));
    }
  }
}