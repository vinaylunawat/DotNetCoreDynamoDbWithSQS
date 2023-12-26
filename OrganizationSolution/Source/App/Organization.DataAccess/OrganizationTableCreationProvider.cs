namespace Organization.DataAccess
{
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.Model;
    using Framework.DataAccess.Repository;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Organization.Entity;

    public class OrganizationTableCreationProvider : DynamoDBClientProviderBase
    {
        private readonly ILogger<DynamoDBClientProviderBase> _logger;
        private readonly IAmazonDynamoDB _client;
        private readonly IConfiguration _configuration;
        private const string TableName = "Employee";

        public OrganizationTableCreationProvider(ILogger<DynamoDBClientProviderBase> logger, IAmazonDynamoDB amazonDynamoDBClient)
            : base(logger, amazonDynamoDBClient)
        {
            _logger = logger;
            _client = amazonDynamoDBClient;
        }

        public override async Task CreateTable()
        {
            Employee employee;
            var request = new CreateTableRequest
            {
                TableName = TableName,
                AttributeDefinitions = new List<AttributeDefinition>()
                    {
                        new AttributeDefinition
                        {
                                AttributeName = nameof(employee.Id),
                                AttributeType = ScalarAttributeType.S
                        },
                        //new AttributeDefinition
                        //{
                        //        AttributeName =nameof(employee.Name),
                        //        AttributeType =ScalarAttributeType.S
                        //}
                    },
                KeySchema = new List<KeySchemaElement>()
                        {
                             new KeySchemaElement
                             {
                                    AttributeName = nameof(employee.Id),
                                    KeyType =KeyType.HASH
                                },
                             //new KeySchemaElement
                             //   {
                             //       AttributeName = nameof(employee.Name),
                             //       KeyType =KeyType.RANGE
                             //   },
                        },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 10,
                    WriteCapacityUnits = 5
                }
            };
            await _client.CreateTableAsync(request);
        }
    }
}
