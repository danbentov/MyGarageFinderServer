namespace MyGarageFinderServer.DTO
{
    public class ChatMessageDTO
    {
        public int MessageID { get; set; }
        public string MessageText { get; set; }
        public int UserID { get; set; }
        public int GarageID { get; set; }
        public int MessageSenderID { get; set; }
        public DateTime MessageTimestamp { get; set; }
        public bool IsRead { get; set; }
    }
}
