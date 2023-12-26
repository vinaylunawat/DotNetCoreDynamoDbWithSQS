namespace Organization.Business.Employee.Manager
{
    using Amazon.SQS.Model;
    using AutoMapper;
    using Framework.Business;
    using Framework.Business.Manager.Command;
    using Framework.Business.ServiceProvider.Queue;
    using Framework.Configuration.Models;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Organization.Business.Country.Manager;
    using Organization.Business.Employee.Models;
    using Organization.Business.Employee.Validators;
    using Organization.DataAccess;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="EmployeeCommandManager" />.
    /// </summary>
    public class EmployeeCommandManager : CommandManager<Guid, EmployeeErrorCode, Entity.Employee, EmployeeCreateModel, EmployeeUpdateModel>, IEmployeeCommandManager
    {
        /// <summary>
        /// Defines the _EmployeeQueryRepository.
        /// </summary>
        private readonly IEmployeeQueryRepository _employeeQueryRepository;

        /// <summary>
        /// Defines the _EmployeeCommandRepository.
        /// </summary>
        private readonly IEmployeeCommandRepository _employeeCommandRepository;

        private readonly IQueueManager<AmazonSQSConfigurationOptions, List<Message>> _queueManager;

        private readonly AmazonSQSConfigurationOptions _amazonSQSConfigurationOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeCommandManager"/> class.
        /// </summary>
        /// <param name="createModelValidator">The createModelValidator<see cref="EmployeeCreateModelValidator"/>.</param>
        /// <param name="updateModelValidator">The updateModelValidator<see cref="EmployeeUpdateModelValidator"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{EmployeeCommandManager}"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        /// <param name="EmployeeQueryRepository">The EmployeeQueryRepository<see cref="IEmployeeQueryRepository"/>.</param>
        /// <param name="EmployeeCommandRepository">The EmployeeCommandRepository<see cref="IEmployeeCommandRepository"/>.</param>
        public EmployeeCommandManager(
            EmployeeCreateModelValidator createModelValidator,
            EmployeeUpdateModelValidator updateModelValidator,
            ILogger<EmployeeCommandManager> logger,
            IMapper mapper
            , IEmployeeQueryRepository employeeQueryRepository
            , IEmployeeCommandRepository employeeCommandRepository
            , IQueueManager<AmazonSQSConfigurationOptions, List<Message>> queueManager
            , ApplicationOptions options
            )
            : base(employeeQueryRepository, employeeCommandRepository, createModelValidator, updateModelValidator, logger, mapper, EmployeeErrorCode.IdDoesNotExist, EmployeeErrorCode.IdNotUnique)
        {
            _employeeQueryRepository = employeeQueryRepository;
            _employeeCommandRepository = employeeCommandRepository;
            _queueManager = queueManager;
            _amazonSQSConfigurationOptions = options.amazonSQSConfigurationOptions;
        }

        protected override async Task<ErrorRecords<Guid, EmployeeErrorCode>> CreateValidationAsync(EmployeeCreateModel model, CancellationToken cancellationToken)
        {
            var errorRecords = await base.CreateValidationAsync(model, cancellationToken).ConfigureAwait(false);
            var result = await _employeeQueryRepository.FindEmployeeByName(model.Name, cancellationToken);
            if (result.Any(item => item.Name == model.Name))
            {
                errorRecords.Add(new ErrorRecord<Guid, EmployeeErrorCode>(EmployeeErrorCode.NameShouldUnique, "name should be unique"));
            }
            return errorRecords;
        }

        protected override async Task<ErrorRecords<Guid, EmployeeErrorCode>> UpdateValidationAsync(EmployeeUpdateModel updateModel, CancellationToken cancellationToken)
        {
            var errorRecords = await base.UpdateValidationAsync(updateModel, cancellationToken).ConfigureAwait(false);
            var result = await _employeeQueryRepository.FindEmployeeByName(updateModel.Name, cancellationToken);
            if (result.Any(item => item.Id != updateModel.Id))
            {
                errorRecords.Add(new ErrorRecord<Guid, EmployeeErrorCode>(EmployeeErrorCode.NameShouldUnique, "name should be unique"));
            }
            return errorRecords;
        }

        protected override async Task<ErrorRecords<Guid, EmployeeErrorCode>> DeleteValidationAsync(Guid key, CancellationToken cancellationToken)
        {
            var errorRecords = await base.DeleteValidationAsync(key, cancellationToken).ConfigureAwait(false);
            var result = await _employeeQueryRepository.GetByKey(key, cancellationToken);
            if (result == null)
            {
                errorRecords.Add(new ErrorRecord<Guid, EmployeeErrorCode>(EmployeeErrorCode.IdDoesNotExist, "Key not found"));
            }
            return errorRecords;
        }

        protected override Task CreateAfterSaveAsync(EmployeeCreateModel model, Entity.Employee entitie, CancellationToken cancellationToken)
        {
            _queueManager.SendMessageAsync(_amazonSQSConfigurationOptions, JsonConvert.SerializeObject(model), cancellationToken);
            return Task.CompletedTask;
        }

    }
}
