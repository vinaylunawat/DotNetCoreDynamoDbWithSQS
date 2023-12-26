namespace Framework.Business.Manager.Query
{
    using Framework.Business.Manager;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    public interface IQueryManager<TReadModel> : IManagerBase
    where TReadModel : class
    {
        Task<IEnumerable<TReadModel>> GetAll(CancellationToken cancellationToken);
        Task<TReadModel> GetByKey<TKey>(TKey key, CancellationToken cancellationToken);

    }
}
