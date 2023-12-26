namespace Organization.API.Controllers
{
    using EnsureThat;
    using Framework.Service;
    using Microsoft.AspNetCore.Mvc;
    using Organization.Business.Employee.Manager;
    using Organization.Business.Employee.Models;
    using Organization.Service;

    [ApiController]
    [ApiExplorerSettings(GroupName = ApiConstants.ApiVersion)]
    [Produces(SupportedContentTypes.Json, SupportedContentTypes.Xml)]
    [QueryRoute]
    public class EmployeeQueryController : ControllerBase
    {
        private readonly IEmployeeQueryManager _manager;

        public EmployeeQueryController(IEmployeeQueryManager manager)
        {
            EnsureArg.IsNotNull(manager, nameof(manager));

            _manager = manager;
        }

        [HttpGet(nameof(GetAll))]
        [ProducesResponseType(typeof(IEnumerable<EmployeeReadModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _manager.GetAll(cancellationToken).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet(nameof(GetByKey))]
        [ProducesResponseType(typeof(IEnumerable<EmployeeReadModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByKey(Guid key, CancellationToken cancellationToken)
        {
            var result = await _manager.GetByKey(key, cancellationToken).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
