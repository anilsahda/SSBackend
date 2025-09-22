using Amazon.DynamoDBv2.DataModel;

namespace DigitalLibraryAdminService.Data.Entities
{
    [DynamoDBTable("IssueReports")]
    public class IssueReport
    {
        [DynamoDBHashKey]
        public int Id { get; set; }
        [DynamoDBProperty]
        public int BookId {  get; set; }
        [DynamoDBProperty]
        public DateTime IssueDate { get; set; }
        [DynamoDBProperty]
        public int IssueBookId {  get; set; }
    }
}
