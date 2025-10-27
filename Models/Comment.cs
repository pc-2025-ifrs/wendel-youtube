namespace Youtube.Api.Models
{
  public record class Comment
  {
    public string Id { get; init; }
    public string Content { get; set; }
    public uint Likes { get; set; }
    public uint Deslikes { get; set; }
    private DateTime Date { get; init; }
    public Channel Channel { get; init; }
    public List<Comment> Comments { get; set; } = [];
    public bool Edited { get; set; } = false;

    public Video Video { get; set;}

    public Comment(string content, Video video, Channel channel)
    {
      Id = Utils.RandomId();
      Content = content;
      Date = DateTime.Now;
      Channel = channel;
      Video = video;
    }

    public void EditComment(string newContent)
    {
      Content = newContent;
      Edited = true;
    }

    public string GetTime()
    {
      TimeSpan timeSinceComment = DateTime.Now - Date;
      string time = "";

      if (timeSinceComment.TotalSeconds < 60)
      {
        time = $"{(int)timeSinceComment.TotalSeconds} seconds ago";
      }
      else if (timeSinceComment.TotalMinutes < 60)
      {
        time = $"{(int)timeSinceComment.TotalMinutes} minutes ago";
      }
      else if (timeSinceComment.TotalHours < 24)
      {
        time = $"{(int)timeSinceComment.TotalHours} hours ago";
      }
      else if (timeSinceComment.TotalDays < 30)
      {
        time = $"{(int)timeSinceComment.TotalDays} days ago";
      }
      else if (timeSinceComment.TotalDays < 365)
      {
        int months = (int)(timeSinceComment.TotalDays / 30);
        time = $"{months} months ago";
      }
      else
      {
        int years = (int)(timeSinceComment.TotalDays / 365);
        time = $"{years} years ago";
      }
      string edited = Edited ? "(edited)" : "";
      return $"{time} {edited}";
    }

    public void AddLike()
    {
      Likes++;
    }

    public void AddDeslike()
    {
      Deslikes++;
    }

    public void AddComment(Comment comment)
    {
      Comments.Add(comment);
    }

  }
}