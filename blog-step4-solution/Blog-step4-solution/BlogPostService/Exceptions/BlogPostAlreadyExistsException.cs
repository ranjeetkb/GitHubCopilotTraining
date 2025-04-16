namespace BlogPostService.Exceptions
{
    public class BlogPostAlreadyExistsException:ApplicationException
    {
        public BlogPostAlreadyExistsException(string message) : base(message) { }
       
    }
}
