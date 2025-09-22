using SSHouseAdminService.Data.Entities;

public class Owner
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation Properties
    public ICollection<PostHouse> PostHouses { get; set; }
    public ICollection<Complain> Complains { get; set; }
    public ICollection<Message> Messages { get; set; }
}