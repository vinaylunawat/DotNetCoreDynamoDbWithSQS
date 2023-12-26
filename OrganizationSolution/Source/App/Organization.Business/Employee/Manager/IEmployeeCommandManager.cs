namespace Organization.Business.Country.Manager
{

    using Framework.Business.Manager.Command;
    using Organization.Business.Employee;
    using Organization.Business.Employee.Models;    

    /// <summary>
    /// Defines the <see cref="ICountryCommandManager" />.
    /// </summary>
    public interface IEmployeeCommandManager : ICommandManager<Guid, EmployeeErrorCode, EmployeeCreateModel, EmployeeUpdateModel>
    {
    }
}
