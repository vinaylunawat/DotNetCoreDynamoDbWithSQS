namespace Organization.Entity
{
    using Amazon.DynamoDBv2.DataModel;
    using Framework.Entity.Entities;

    [DynamoDBTable("Employee")]
    public class Employee : EntityWithId<Guid>
    {
        [DynamoDBProperty("Name")]
        public string? Name { get; set; }

        [DynamoDBProperty("Designation")]
        public string? Designation { get; set; }

        [DynamoDBProperty("Age")]
        public int Age { get; set; }
    }
}
