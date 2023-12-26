namespace Framework.DataAccess.Repository
{
    using Amazon.DynamoDBv2.DataModel;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class GenericCommandRepository<TEntity> : RepositoryBase<TEntity>, IGenericCommandRepository<TEntity>
    {
        public GenericCommandRepository(IDynamoDBContext context) : base(context)
        {
        }
        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {            
            await _dynamoDBContext.SaveAsync(entity, cancellationToken);
            return entity;
        }

        public async Task DeleteAsync<TKey>(TKey key, CancellationToken cancellationToken)
        {
            await _dynamoDBContext.DeleteAsync<TEntity>(key, cancellationToken);
        }
        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dynamoDBContext.SaveAsync(entity, cancellationToken);
        }
    }
}
