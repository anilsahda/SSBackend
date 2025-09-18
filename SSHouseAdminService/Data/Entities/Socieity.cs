namespace SSHouseAdminService.Data.Entities
{
    public class Society
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public ICollection<House> Houses { get; set; } = new List<House>();
    }
}
