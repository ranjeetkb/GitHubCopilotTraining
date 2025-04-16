using CommentService.Models;
namespace CommentService.Repository
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetAllComments();
        IEnumerable<Comment> GetCommentsByBlogPostId(int blogPostId);
        Comment GetCommentById(int id);
        Comment CreateComment(Comment comment);
        bool UpdateComment(int id, Comment comment);
        bool DeleteComment(int id);
    }
}
