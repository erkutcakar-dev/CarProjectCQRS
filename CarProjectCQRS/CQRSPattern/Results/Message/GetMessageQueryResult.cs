namespace CarProjectCQRS.CQRSPattern.Results.Message
{
    public class GetMessageQueryResult
    {
        public int MessageId { get; set; }
        public string SenderMail { get; set; }
        public string Telephone { get; set; }
        public string MessageText { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
    }
}
