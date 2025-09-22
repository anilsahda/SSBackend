namespace SSHouseAdminService.Data.Entities
{
    public class House
    {
        public int Id { get; set; }
        public string HouseNumber { get; set; } = null!;
        public int SocietyId { get; set; }
        public Society? Society { get; set; }
        public string Address { get; set; } = null!;
        public bool IsAllocated { get; set; } = false;

        public ICollection<HouseReport> HouseReports { get; set; } = new List<HouseReport>();
        public ICollection<AllocateHouse> Allocations { get; set; } = new List<AllocateHouse>();
        public ICollection<SellHouseReport> SellReports { get; set; } = new List<SellHouseReport>();
        public ICollection<RentHouseReport> RentReports { get; set; } = new List<RentHouseReport>();
        public ICollection<Complain> Complains { get; set; } = new List<Complain>();



    }
}
