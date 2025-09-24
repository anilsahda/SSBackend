using Amazon.DynamoDBv2.DataModel;

namespace DigitalLibraryAdminService.Data.Entities
{
    [DynamoDBTable("Books")]
    public class Book
    {
        [DynamoDBHashKey]
        public int Id { get; set; }
        [DynamoDBProperty]
        public string Name { get; set; }
        [DynamoDBProperty]
        public string Author { get; set; }
        [DynamoDBProperty]
        public int BranchId{ get; set; }
        [DynamoDBProperty]
        public decimal Price {  get; set; }
        [DynamoDBProperty]
        public int Quantity { get; set; }
        [DynamoDBProperty]
        public int PublicationId { get; set; }
        [DynamoDBProperty]
        public string Details { get; set; }
    }
}
