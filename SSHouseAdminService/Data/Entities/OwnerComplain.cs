public class OwnerComplain
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    // Foreign Key
    public int OwnerId { get; set; }

    // Navigation Property
    public Owner Owner { get; set; }
}