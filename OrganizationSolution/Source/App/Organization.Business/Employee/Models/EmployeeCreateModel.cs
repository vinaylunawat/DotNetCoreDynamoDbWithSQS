using Framework.Business.Models;
using Framework.Business.Models.Models;

namespace Organization.Business.Employee.Models
{
    /// <summary>
    /// Defines the <see cref="EmployeeCreateModel" />.
    /// </summary>
    public class EmployeeCreateModel : Model, IModel
    {
        public EmployeeCreateModel()
        {

        }
        public EmployeeCreateModel(string name, int age, string designation)
        {
            Name = name;
            Age = age;
            Designation = designation;
        }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Designation { get; set; }
    }
}
