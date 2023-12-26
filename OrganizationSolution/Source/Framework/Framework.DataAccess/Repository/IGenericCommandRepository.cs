namespace Framework.DataAccess.Repository
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IGenericCommandRepository<TEntity>
    {
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        Task DeleteAsync<TKey>(TKey key, CancellationToken cancellationToken);
    }
}
