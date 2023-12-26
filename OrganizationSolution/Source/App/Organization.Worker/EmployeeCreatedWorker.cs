namespace Organization.Worker
{
    using Amazon.SQS.Model;    
    using Framework.Business.ServiceProvider.Queue;
    using Framework.Configuration.Models;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;    
    using Framework.Business.Extension;
    using Organization.Business.Employee.Models;

    public class EmployeeCreatedWorker : MessagehandlerBase<EmployeeCreateModel>
    {
        public EmployeeCreatedWorker(ILogger<EmployeeCreatedWorker> logger, ApplicationOptions applicationOptions, IQueueManager<AmazonSQSConfigurationOptions, List<Message>> queueManger)
            : base(logger, applicationOptions, queueManger)
        {
        }

        protected override Task MessageHandlerAsync(EmployeeCreateModel message, CancellationToken cancellationToken)
        {
            _logger.LogInformation("message received {0}", JsonConvert.SerializeObject(message));
            return Task.CompletedTask;
        }
    }
}
