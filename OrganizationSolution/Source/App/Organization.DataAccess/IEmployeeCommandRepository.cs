namespace Organization.DataAccess
{
    using Framework.DataAccess.Repository;
    using Organization.Entity;    

    public interface IEmployeeCommandRepository : IGenericCommandRepository<Employee>
    {
    }
}
