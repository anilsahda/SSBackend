namespace SSHouseAdminService.Data.Entities
{
    public class Complain
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public House House { get; set; } = null!;
        public int? MemberId { get; set; } // Complain can be anonymous or by member
        public Member? Member { get; set; }
        public string Description { get; set; } = null!;
        public DateTime ComplainDate { get; set; } = DateTime.UtcNow;
        public bool IsResolved { get; set; } = false;
    }
}
