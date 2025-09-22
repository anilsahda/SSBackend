namespace SSHouseAdminService.Data.Entities
{
    public class SellHouseReport
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public House House { get; set; } = null!;
        public decimal SellPrice { get; set; }
        public DateTime SellDate { get; set; } = DateTime.UtcNow;
        public string BuyerName { get; set; } = null!;
    }

}
