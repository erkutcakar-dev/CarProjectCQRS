namespace CarProjectCQRS.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public string SenderMail { get; set; }
        public string Telephone { get; set; }
        public string MessageText { get; set; }
        public DateTime SendDate { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;

    }
}
