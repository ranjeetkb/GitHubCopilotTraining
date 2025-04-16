using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using UserService.Models;

namespace BlogPostService.Models
{
    public class BlogPost
    {
        [BsonId]
        public int BlogPostId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<string> Tags { get; set; } = new List<string>();

        // Reference to the author (user)
        public int AuthorId { get; set; }

        // Navigation property for the author (user)
        public User Author { get; set; }

    }
}
