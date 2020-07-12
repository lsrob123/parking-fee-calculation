namespace CarPark.Models
{
    public class Error
    {
        public Error()
        {
        }

        public Error(string source, string message)
        {
            Source = source;
            Message = message;
        }

        public string Message { get; set; }
        public string Source { get; set; }
    }
}