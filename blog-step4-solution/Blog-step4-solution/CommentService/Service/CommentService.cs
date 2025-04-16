using CommentService.Models;
using CommentService.Repository;

namespace CommentService.Service
{
    public class CommentService:ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return _commentRepository.GetAllComments();
        }

        public IEnumerable<Comment> GetCommentsByBlogPostId(int blogPostId)
        {
            return _commentRepository.GetCommentsByBlogPostId(blogPostId);
        }

        public Comment GetCommentById(int id)
        {
            return  _commentRepository.GetCommentById(id);
        }

        public Comment CreateComment(Comment comment)
        {
            _commentRepository.CreateComment(comment);
            return comment;
        }

        public bool UpdateComment(int id, Comment comment)
        {
            return _commentRepository.UpdateComment(id, comment);
        }

        public bool DeleteComment(int id)
        {
            return _commentRepository.DeleteComment(id);
        }
    }
}
