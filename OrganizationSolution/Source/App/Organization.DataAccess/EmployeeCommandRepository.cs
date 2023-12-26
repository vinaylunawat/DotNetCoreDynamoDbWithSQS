namespace Organization.DataAccess
{
    using Amazon.DynamoDBv2.DataModel;
    using Framework.DataAccess.Repository;
    using Organization.Entity;
    
    public class EmployeeCommandRepository : GenericCommandRepository<Employee>, IEmployeeCommandRepository
    {
        public EmployeeCommandRepository(IDynamoDBContext context) : base(context)
        {
        }

    }
}
