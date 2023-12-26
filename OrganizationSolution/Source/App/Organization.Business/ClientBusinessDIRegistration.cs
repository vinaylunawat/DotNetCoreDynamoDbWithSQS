namespace Organization.Business
{
    using Framework.Business;
    using Geography.Business.Country;    
    using Microsoft.Extensions.DependencyInjection;
    using Organization.Business.Employee.Manager;

    /// <summary>
    /// Defines the <see cref="ClientBusinessDIRegistration" />.
    /// </summary>
    public static class ClientBusinessDIRegistration
    {
        /// <summary>
        /// The ConfigureBusinessServices.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ConfigureBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EmployeeMappingProfile).Assembly);
            services.AddManagers(typeof(EmployeeQueryManager).Assembly);
            return services;
        }
    }
}
