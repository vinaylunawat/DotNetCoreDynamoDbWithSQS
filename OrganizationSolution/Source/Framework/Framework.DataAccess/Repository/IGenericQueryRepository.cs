namespace Framework.DataAccess.Repository
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IGenericQueryRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken);

        Task<TEntity> GetByKey<TKey>(TKey Id, CancellationToken cancellationToken);

        Task<TEntity> GetByKey<TKey, TRangeKey>(TKey key, TRangeKey rangeKey, CancellationToken cancellationToken);
    }
}
