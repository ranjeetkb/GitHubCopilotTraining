using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomExceptions;
using Entities;

namespace Service
{
    public class LikeRepository
    {      

        private List<Like> likes;

        public LikeRepository()
        {
            likes = new List<Like>();
        }

        public void CreateLike(int userId, int postId)
        {
            Like like = new Like(userId, postId);
            likes.Add(like);
        }

        public List<Like> GetLikesForPost(int postId)
        {
            return likes.Where(like => like.PostId == postId).ToList();
        }

        public List<Like> GetLikesByUser(int userId)
        {
            return likes.Where(like => like.UserId == userId).ToList();
        }

        public void DeleteLike(int userId, int postId)
        {
            likes.RemoveAll(like => like.UserId == userId && like.PostId == postId);
        }
    }
}
