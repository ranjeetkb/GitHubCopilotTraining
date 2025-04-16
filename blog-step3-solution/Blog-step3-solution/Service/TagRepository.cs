using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using CustomExceptions;

namespace Service
{
    public class TagRepository
    {
        private List<Tag> _tags;        

        public TagRepository()
        {
            _tags = new List<Tag>();
         
        }

        public Tag CreateTag(int id,string name)
        {
            var tag = new Tag(id, name);
            _tags.Add(tag);
            return tag;
        }

        public Tag GetTagById(int tagId)
        {
            return _tags.FirstOrDefault(tag => tag.TagId == tagId);
        }

        public List<Tag> GetTagsByName(string name)
        {
            return _tags.Where(tag => tag.Name == name).ToList();
        }

        public void UpdateTag(int tagId, string newName)
        {
            var tag = GetTagById(tagId);
            if (tag != null)
            {
                tag.Name = newName;
            }
        }

        public void DeleteTag(int tagId)
        {
            _tags.RemoveAll(tag => tag.TagId == tagId);
        }
    }
}