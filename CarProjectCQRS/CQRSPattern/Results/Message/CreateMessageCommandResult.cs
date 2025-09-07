namespace CarProjectCQRS.CQRSPattern.Results.Message
{
    public class CreateMessageCommandResult
    {
        public int MessageId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
