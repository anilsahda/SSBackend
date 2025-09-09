using Amazon.DynamoDBv2.DataModel;

namespace SuperAdminService.Data.Entities
{
    [DynamoDBTable("UserRoles")]
    public class UserRole
    {
        [DynamoDBHashKey]
        public int Id { get; set; }

        [DynamoDBProperty]
        public int UserId { get; set; }

        [DynamoDBProperty]
        public int RoleId { get; set; }
    }
}
