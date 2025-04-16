using CommentService.Exceptions;
using CommentService.Models;
using MongoDB.Driver;

namespace CommentService.Repository
{
    public class CommentRepository:ICommentRepository
    {
        private readonly IMongoCollection<Comment> _commentsCollection;
        private readonly CommentDbSettings _settings;
        public CommentRepository(CommentDbSettings dbSettings)
        {
            _settings = dbSettings;
            _commentsCollection = _settings.commentCollection;

        }
        public IEnumerable<Comment> GetAllComments()
        {
              return _commentsCollection.Find(_ => true).ToList();
        }

        public IEnumerable<Comment> GetCommentsByBlogPostId(int blogPostId)
        {
            
            var result=_commentsCollection.Find(comment => comment.BlogPostId == blogPostId).ToList();
            
            return result;

        }

        public Comment GetCommentById(int id)
        {
           
             var comment = _commentsCollection.Find(comment => comment.CommentId == id).FirstOrDefault();
             if (comment == null)
                 throw new CommentNotFoundException($"Comment with id {id} not found");
             return comment;
            
        }

        public  Comment CreateComment(Comment comment)
        {
            try
            {
                _commentsCollection.InsertOne(comment);
                return comment;
            }
            catch (Exception)
            {
                throw new CommentAlreadyExistsException($"Comment with id {comment.CommentId} already exixts");
            }
        }

        public bool UpdateComment(int id, Comment comment)
        {
           
             var result = _commentsCollection.ReplaceOne(c => c.CommentId == id, comment);
             if (result.ModifiedCount == 0)
                throw new CommentNotFoundException($"Comment with id {id} not found");
             return result.IsAcknowledged && result.ModifiedCount > 0;
           
             
        }

        public bool DeleteComment(int id)
        {
           
              var result = _commentsCollection.DeleteOne(c => c.CommentId == id);
              if (result.DeletedCount == 0)
                throw new CommentNotFoundException($"Comment with id {id} not found");
              return result.IsAcknowledged && result.DeletedCount > 0;
            
        }
    }
}
