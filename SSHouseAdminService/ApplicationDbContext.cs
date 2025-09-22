using Microsoft.EntityFrameworkCore;
using SSHouseAdminService.Data.Entities;

namespace SSHouseAdminService
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<AllocateHouse> AllocateHouses { get; set; }
        public DbSet<Complain> Complains { get; set; }
        public DbSet<HouseReport> HouseReports { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<MemberReport> MemberReports { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<RentHouseReport> RentHouseReports { get; set; }
        public DbSet<SellHouseReport> SellHouseReports { get; set; }
        public DbSet<Society> Societies { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<PostHouse> PostHouses { get; set; }
        public DbSet<OwnerComplain> OwnerComplains { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
