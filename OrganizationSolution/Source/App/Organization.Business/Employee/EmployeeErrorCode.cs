namespace Organization.Business.Employee
{
    /// <summary>
    /// Defines the CountryErrorCode.
    /// </summary>
    public enum EmployeeErrorCode
    { 
        UnknownError = 0,         
        AgeMustBeGreaterThanZero,        
        IdDoesNotExist,        
        NameRequired,       
        NameTooLong,     
        NameShouldUnique,
        IdNotUnique,
        DesignationRequired,
        IdMustNotBeEmpty
    }
}
