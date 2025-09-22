namespace SSHouseAdminService.Data.Entities
{
    public class AllocateHouse
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public House House { get; set; } = null!;
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public DateTime AllocationDate { get; set; } = DateTime.UtcNow;
        public DateTime? ReleaseDate { get; set; }
    }
}
