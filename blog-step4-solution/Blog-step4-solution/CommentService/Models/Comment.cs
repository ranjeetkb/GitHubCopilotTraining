using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CommentService.Models
{
    public class Comment
    {
        [BsonId]
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;        

        // Referencing BlogPost Id
        public int BlogPostId { get; set; }


    }
}
