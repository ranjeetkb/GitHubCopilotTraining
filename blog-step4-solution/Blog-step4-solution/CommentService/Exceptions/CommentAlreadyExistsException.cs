namespace CommentService.Exceptions
{
    public class CommentAlreadyExistsException:ApplicationException
    {
        public CommentAlreadyExistsException(string message) : base(message) { }
       
    }
}
