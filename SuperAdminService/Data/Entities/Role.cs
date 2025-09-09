using Amazon.DynamoDBv2.DataModel;

namespace SuperAdminService.Data.Entities
{
    [DynamoDBTable("Roles")]
    public class Role
    {
        [DynamoDBHashKey]
        public int Id { get; set; }

        [DynamoDBProperty]
        public string Name { get; set; }
    }
}
