namespace SSHouseAdminService.Data.Entities
{
    public class RentHouseReport
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public House House { get; set; } = null!;
        public decimal RentAmount { get; set; }
        public DateTime RentStartDate { get; set; }
        public DateTime? RentEndDate { get; set; }
        public string RenterName { get; set; } = null!;
    }
}
