using Youtube.Api.Models;

namespace Youtube.Api.Repositories
{
  public class InMemoryCommentRepository : ICommentRepository
  {
    private static readonly List<Comment> _comments = [];

    public void Add(Comment comment)
    {
      _comments.Add(comment);
    }

        public IEnumerable<Comment> GetAll()
        {
            return _comments;
        }
    
    public Comment? GetById(string id)
    {
      return FindCommentRecursive(_comments, id);
    }

    private Comment? FindCommentRecursive(IEnumerable<Comment> commentsToSearch, string id)
    {
      foreach (var comment in commentsToSearch)
      {
        if (comment.Id == id)
        {
          return comment;
        }

        var foundInReplies = FindCommentRecursive(comment.Comments, id);
        if (foundInReplies != null)
        {
          return foundInReplies;
        }
      }
      
      return null;
    }
  }
}