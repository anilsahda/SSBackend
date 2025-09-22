using Amazon.DynamoDBv2.DataModel;

namespace DigitalLibraryAdminService.Data.Entities
{
    [DynamoDBTable ("Students")]
    public class Student
    {
        [DynamoDBHashKey]
        public int Id { get; set; }
        [DynamoDBProperty]
        public string Name { get; set; }
        [DynamoDBProperty]
        public int BranchId { get; set; }
        [DynamoDBProperty]
        public string Mobile {  get; set; }
        [DynamoDBProperty]
        public string Email {  get; set; }
        [DynamoDBProperty]
        public string Address {  get; set; }
        [DynamoDBProperty]
        public DateTime DOB { get; set; }


    }
}
