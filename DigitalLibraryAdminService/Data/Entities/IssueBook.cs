using Amazon.DynamoDBv2.DataModel;

namespace DigitalLibraryAdminService.Data.Entities
{
    [DynamoDBTable("IssueBooks")]
    public class IssueBook
    {
        [DynamoDBHashKey]
        public int Id { get; set; }
        [DynamoDBProperty]
        public int BookId {  get; set; }
        [DynamoDBProperty]
        public int PublicationId {  get; set; }
        [DynamoDBProperty]
        public int BranchId {  get; set; }
        [DynamoDBProperty]
        public int StudentId { get; set; }
        [DynamoDBProperty]
        public int IssueDays {  get; set; }
        [DynamoDBProperty]
        public DateTime IssueDate {  get; set; }

    }
}
