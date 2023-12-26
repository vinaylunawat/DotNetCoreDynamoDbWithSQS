namespace Framework.DataAccess.Repository
{
    using Amazon.DynamoDBv2.DataModel;
    using Amazon.DynamoDBv2.DocumentModel;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class GenericQueryRepository<TEntity> : RepositoryBase<TEntity>, IGenericQueryRepository<TEntity>
        where TEntity : class
    {
        public GenericQueryRepository(IDynamoDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken)
        {
            return await _dynamoDBContext.ScanAsync<TEntity>(default).GetRemainingAsync(cancellationToken);
        }

        public async Task<TEntity> GetByKey<TKey>(TKey key, CancellationToken cancellationToken)
        {
            return await _dynamoDBContext.LoadAsync<TEntity>(key, cancellationToken);
        }

        public async Task<TEntity> GetByKey<TKey, TRangeKey>(TKey key, TRangeKey rangeKey, CancellationToken cancellationToken)
        {
            return await _dynamoDBContext.LoadAsync<TEntity>(key, rangeKey, cancellationToken);
        }
    }
}
