namespace SSHouseAdminService.Data.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public ICollection<AllocateHouse> AllocatedHouses { get; set; } = new List<AllocateHouse>();
        public ICollection<MemberReport> MemberReports { get; set; } = new List<MemberReport>();
        public ICollection<Complain> Complains { get; set; } = new List<Complain>();
    }
}
