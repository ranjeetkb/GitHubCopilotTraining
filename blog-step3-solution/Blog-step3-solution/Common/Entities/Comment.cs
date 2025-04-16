using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    // The class "Comment" will be acting as the data model for the Comment.
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PostId { get; set; } // ID of the blog post this comment belongs to

        public Comment(int id, string content, DateTime createdAt, int postId)
        {
            Id = id;
            Content = content;
            CreatedAt = createdAt;
            PostId = postId;
        }
    }
   
}
