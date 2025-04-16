using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions
{
    public class UserNotFoundException:ApplicationException
    {
        public UserNotFoundException(){        
        }
        public UserNotFoundException(string message) : base(message){
        }

    }
    public class UserAlreadyExistsException : ApplicationException
    {
        public UserAlreadyExistsException()
        {
        }
        public UserAlreadyExistsException(string message) : base(message)
        {
        }

    }
}
