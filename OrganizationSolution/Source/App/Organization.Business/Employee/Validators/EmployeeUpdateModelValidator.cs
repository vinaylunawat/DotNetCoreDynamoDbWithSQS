namespace Organization.Business.Employee.Validators
{
    using FluentValidation;
    using Framework.Business.Extension;
    using Framework.Constant;
    using Geography.Business.Country;
    using Organization.Business.Employee.Models;

    /// <summary>
    /// Defines the <see cref="CountryUpdateModelValidator" />.
    /// </summary>
    public class EmployeeUpdateModelValidator : EmployeeBaseModelValidator<EmployeeUpdateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryUpdateModelValidator"/> class.
        /// </summary>
        public EmployeeUpdateModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithErrorEnum(EmployeeErrorCode.IdMustNotBeEmpty);

        }
    }
}
