namespace Organization.Service.Controllers.Command
{
    using EnsureThat;
    using Framework.Business;
    using Framework.Business.Extension;
    using Framework.Service;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Organization.Business.Country.Manager;
    using Organization.Business.Employee;
    using Organization.Business.Employee.Models;
    using Organization.Service;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="EmployeeCommandController" />.
    /// </summary>
    [ApiController]
    [Produces(SupportedContentTypes.Json, SupportedContentTypes.Xml)]
    [ApiExplorerSettings(GroupName = ApiConstants.ApiVersion)]
    [Consumes(SupportedContentTypes.Json, SupportedContentTypes.Xml)]
    [CommandRoute]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class EmployeeCommandController : ControllerBase
    {
        /// <summary>
        /// Defines the _manager.
        /// </summary>
        private readonly IEmployeeCommandManager _manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeCommandController"/> class.
        /// </summary>
        /// <param name="manager">The manager<see cref="IEmployeeCommandManager"/>.</param>
        public EmployeeCommandController(IEmployeeCommandManager manager)
        {
            EnsureArg.IsNotNull(manager, nameof(manager));
            _manager = manager;
        }

        /// <summary>
        /// Creates the specified countries.
        /// </summary>
        /// <param name="countries">The countries.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpPost(nameof(Create))]
        [ProducesResponseType(typeof(ManagerResponse<Guid, EmployeeErrorCode>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ManagerResponse<Guid, EmployeeErrorCode>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([Required] EmployeeCreateModel model, CancellationToken cancellationToken)
        {
            var result = await _manager.CreateAsync(model, cancellationToken).ConfigureAwait(false);

            return result.ToStatusCode();
        }



        /// <summary>
        /// Updates the specified Employee(s).
        /// </summary>
        /// <param name="EmployeeUpdateModels">Employee.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpPut(nameof(Update))]
        [ProducesResponseType(typeof(ManagerResponse<Guid, EmployeeErrorCode>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ManagerResponse<Guid, EmployeeErrorCode>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([Required] EmployeeUpdateModel employeeUpdateModel, CancellationToken cancellationToken)
        {
            var result = await _manager.UpdateAsync(employeeUpdateModel, cancellationToken).ConfigureAwait(false);

            return result.ToStatusCode();
        }



        /// <summary>
        /// Deletes the by Id.
        /// </summary>
        /// <param name="ids">The Ids.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpDelete(nameof(DeleteByKey))]
        [ProducesResponseType(typeof(ManagerResponse<Guid, EmployeeErrorCode>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ManagerResponse<Guid, EmployeeErrorCode>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteByKey([Required] Guid key, CancellationToken cancellationToken)
        {
            var result = await _manager.DeleteByKeyAsync(key, cancellationToken).ConfigureAwait(false);
            return result.ToStatusCode();
        }
    }
}
