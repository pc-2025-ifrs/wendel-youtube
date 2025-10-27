using Youtube.Api.Models;

namespace Youtube.Api.Repositories
{
  public interface ICommentRepository
  {
    IEnumerable<Comment> GetAll();
        void Add(Comment comment);
    Comment? GetById(string id);
  }
}