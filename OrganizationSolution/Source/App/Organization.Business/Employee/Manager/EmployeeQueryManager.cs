namespace Organization.Business.Employee.Manager
{
    using AutoMapper;        
    using Framework.Business.Manager.Query;    
    using Microsoft.Extensions.Logging;
    using Organization.Business.Employee.Models;
    using Organization.DataAccess;        

    /// <summary>
    /// Defines the <see cref="EmployeeQueryManager" />.
    /// </summary>
    public class EmployeeQueryManager : QueryManager<Entity.Employee, EmployeeReadModel>, IEmployeeQueryManager
    {
        /// <summary>
        /// Defines the _countryRepository.
        /// </summary>
        private readonly IEmployeeQueryRepository _employeeQueryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeQueryManager"/> class.
        /// </summary>
        /// <param name="countryRepository">The countryRepository<see cref="ICountryQueryRepository"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{CountryQueryManager}"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        public EmployeeQueryManager(IEmployeeQueryRepository employeeQueryRepository, ILogger<EmployeeQueryManager> logger, IMapper mapper)
            : base(employeeQueryRepository, logger, mapper)
        {
            _employeeQueryRepository = employeeQueryRepository;
        }                 
    }
}
