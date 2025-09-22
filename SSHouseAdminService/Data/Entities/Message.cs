public class Message
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime SentAt { get; set; }

    // Foreign Key
    public int OwnerId { get; set; }

    // Navigation Property
    public Owner Owner { get; set; }
}