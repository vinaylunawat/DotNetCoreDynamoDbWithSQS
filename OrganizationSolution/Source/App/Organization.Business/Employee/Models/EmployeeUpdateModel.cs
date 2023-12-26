using Framework.Business.Models;

namespace Organization.Business.Employee.Models
{

    /// <summary>
    /// Defines the <see cref="CountryUpdateModel" />.
    /// </summary>
    public class EmployeeUpdateModel : EmployeeCreateModel, IModelWithKey<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryUpdateModel"/> class.
        /// </summary>
        public EmployeeUpdateModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryUpdateModel"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="code">The code<see cref="string"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="isoCode">The isoCode<see cref="string"/>.</param>
        public EmployeeUpdateModel(Guid id, string name, int age, string designation)
            : base(name, age, designation)
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public Guid Id { get; set; }
    }
}
