namespace SSHouseAdminService.Data.Entities
{
    public class HouseReport
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public House House { get; set; } = null!;
        public string ReportDetails { get; set; } = null!;
        public DateTime ReportDate { get; set; } = DateTime.UtcNow;
    }
}
