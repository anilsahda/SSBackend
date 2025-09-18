namespace SSHouseAdminService.Data.Entities
{
    public class MemberReport
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public string ReportDetails { get; set; } = null!;
        public DateTime ReportDate { get; set; } = DateTime.UtcNow;
    }
}
