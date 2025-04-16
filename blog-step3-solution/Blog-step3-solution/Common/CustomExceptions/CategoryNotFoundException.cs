using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions
{
    public class CategoryNotFoundException:ApplicationException
    {
        public CategoryNotFoundException()
        {
        }
        public CategoryNotFoundException(string message):base(message)
        {
        }
    }
    public class CategoryAlreadyExistsException : ApplicationException
    {
        public CategoryAlreadyExistsException()
        {
        }
        public CategoryAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
