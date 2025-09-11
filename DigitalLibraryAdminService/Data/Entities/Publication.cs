using Amazon.DynamoDBv2.DataModel;

namespace DigitalLibraryAdminService.Data.Entities
{
    [DynamoDBTable("Publications")]
    public class Publication
    {
        [DynamoDBHashKey]
        public int Id { get; set; }
        [DynamoDBProperty]
        public string Name { get; set; }
    }
}
