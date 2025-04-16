namespace BlogPostService.Exceptions
{
    public class BlogPostNotFoundException:ApplicationException
    {
        public BlogPostNotFoundException(string message) : base(message) { }
       
    }
}
