namespace MacrixPracticalTask_Client.Models
{
    public class Error
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
        public Error(string title, int status, string reason)
        {
            Title = title;
            Status = status;
            Reason = reason;
        }
    }
}
