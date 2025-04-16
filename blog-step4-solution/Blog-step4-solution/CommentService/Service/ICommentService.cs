using CommentService.Models;
using System.Collections.Generic;

namespace CommentService.Service
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetAllComments();
        IEnumerable<Comment> GetCommentsByBlogPostId(int blogPostId);
        Comment GetCommentById(int id);
        Comment CreateComment(Comment comment);
        bool UpdateComment(int id, Comment comment);
        bool DeleteComment(int id);
    }
}
