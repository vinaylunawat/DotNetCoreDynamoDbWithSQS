namespace Organization.Business.Employee.Manager
{
    using Framework.Business.Manager.Query;
    using Organization.Business.Employee.Models;

    /// <summary>
    /// Defines the <see cref="IEmployeeQueryManager" />.
    /// </summary>
    public interface IEmployeeQueryManager : IQueryManager<EmployeeReadModel>
    {        
    }
}
