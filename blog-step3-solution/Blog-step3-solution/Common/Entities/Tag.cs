using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    // The class "Tag" will be acting as the data model for the Tags.
    public class Tag
    {

        public int TagId { get; set; }
        public string Name { get; set; }

        public Tag(int tagId, string name)
        {
            TagId = tagId;
            Name = name;
        }
    }
}
