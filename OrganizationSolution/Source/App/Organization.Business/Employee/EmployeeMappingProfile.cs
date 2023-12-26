namespace Geography.Business.Country
{
    using AutoMapper;
    using Organization.Business.Employee.Models;
    using Organization.Entity;

    /// <summary>
    /// Defines the <see cref="CountryMappingProfile" />.
    /// </summary>
    public class EmployeeMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryMappingProfile"/> class.
        /// </summary>
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeReadModel>();

            CreateMap<EmployeeCreateModel, Employee>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<EmployeeUpdateModel, Employee>();

        }
    }
}
