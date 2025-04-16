using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomExceptions;

namespace Service
{
    public class CommentRepository 
    {
        private List<Comment> _comments;        

        public CommentRepository()
        {
            _comments = new List<Comment>();            
        }

        public Comment CreateComment(Comment comm)
        {
            var comment = new Comment(comm.Id, comm.Content, comm.CreatedAt, comm.PostId);
            _comments.Add(comment);
            return comment;
        }

        public Comment GetCommentById(int id)
        {
            return _comments.Find(c=>c.Id==id);
        }

        public List<Comment> GetCommentsForPost(int postId)
        {
            return _comments.Where(c => c.PostId == postId).ToList();
        }

        public void UpdateComment(int id, string newContent)
        {
            var comment = GetCommentById(id);
            if (comment != null)
            {
                comment.Content = newContent;
            }
        }

        public void DeleteComment(int id)
        {
            _comments.RemoveAll(c => c.Id == id);
        }
    }   
    
}
