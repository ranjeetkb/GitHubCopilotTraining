using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    // The class "Like" will be acting as the data model for the Likes.
    public class Like
    {
        public int UserId { get; set; }
        public int PostId { get; set; }

        public Like(int userId, int postId)
        {
            UserId = userId;
            PostId = postId;
        }
    }
}
