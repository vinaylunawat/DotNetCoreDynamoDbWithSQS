namespace Organization.DataAccess
{
    using Amazon.DynamoDBv2.DataModel;
    using Amazon.DynamoDBv2.DocumentModel;
    using Framework.DataAccess.Repository;
    using Organization.Entity;

    public class EmployeeQueryRepository : GenericQueryRepository<Employee>, IEmployeeQueryRepository
    {
        public EmployeeQueryRepository(IDynamoDBContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Employee>> FindEmployeeByName(string name, CancellationToken cancellationToken)
        {
            var scanConditions = new List<ScanCondition>
            {
                new ScanCondition("Name", ScanOperator.Equal, name)
            };
            return await _dynamoDBContext.ScanAsync<Employee>(scanConditions, default).GetRemainingAsync(cancellationToken);
        }
    }
}
