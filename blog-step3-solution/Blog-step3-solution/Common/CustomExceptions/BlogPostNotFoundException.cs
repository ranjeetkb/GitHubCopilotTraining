using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions
{
    
    public class BlogPostNotFoundException:ApplicationException
    {
        public BlogPostNotFoundException(){        
        }
        public BlogPostNotFoundException(string message) : base(message){
        }
    }
    public class BlogPostAlreadyExistsException : ApplicationException
    {
        public BlogPostAlreadyExistsException()
        {
        }
        public BlogPostAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
