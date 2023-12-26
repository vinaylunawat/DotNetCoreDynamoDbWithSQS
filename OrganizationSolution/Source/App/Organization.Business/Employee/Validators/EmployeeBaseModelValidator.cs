namespace Organization.Business.Employee.Validators
{
    using FluentValidation;
    using Framework.Business;
    using Framework.Business.Extension;
    using Framework.Constant;
    using Geography.Business.Country;
    using Organization.Business.Employee.Models;

    /// <summary>
    /// Defines the <see cref="CountryBaseModelValidator{TCountryCreateModel}" />.
    /// </summary>
    /// <typeparam name="TCountryCreateModel">.</typeparam>
    public abstract class EmployeeBaseModelValidator<TEmployeeCreateModel> : ModelValidator<TEmployeeCreateModel>
    where TEmployeeCreateModel : EmployeeCreateModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryBaseModelValidator{TCountryCreateModel}"/> class.
        /// </summary>
        public EmployeeBaseModelValidator()
        {
            RuleFor(x => x.Age)
                .GreaterThan(0).WithErrorEnum(EmployeeErrorCode.AgeMustBeGreaterThanZero);                            

            RuleFor(x => x.Designation)
                .NotEmpty().WithErrorEnum(EmployeeErrorCode.DesignationRequired);                

            RuleFor(x => x.Name)
                .NotEmpty().WithErrorEnum(EmployeeErrorCode.NameRequired)
                .MaximumLength(BaseConstants.DataLengths.Name).WithErrorEnum(EmployeeErrorCode.NameTooLong);
        }
    }
}
