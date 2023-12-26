namespace Organization.DataAccess
{
    using Framework.DataAccess.Repository;
    using Organization.Entity;    

    public interface IEmployeeQueryRepository : IGenericQueryRepository<Employee>
    {
        Task<IEnumerable<Employee>> FindEmployeeByName(string name, CancellationToken cancellationToken);
    }
}
