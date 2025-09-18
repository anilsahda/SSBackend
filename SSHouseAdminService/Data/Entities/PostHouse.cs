public class PostHouse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Address { get; set; }

    // Foreign Key
    public int OwnerId { get; set; }

    // Navigation Property
    public Owner Owner { get; set; }
}