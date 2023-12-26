namespace Organization.Business.Employee.Models
{
    using Framework.Business.Models;

    /// <summary>
    /// Defines the <see cref="CountryReadModel" />.
    /// </summary>
    public class EmployeeReadModel : EmployeeUpdateModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryReadModel"/> class.
        /// </summary>
        public EmployeeReadModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryReadModel"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="code">The code<see cref="string"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="isoCode">The isoCode<see cref="string"/>.</param>
        public EmployeeReadModel(Guid id, string code, int age, string designation)
            : base(id, code, age, designation)
        {
        }
    }
}
