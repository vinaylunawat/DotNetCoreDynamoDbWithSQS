namespace Framework.Business.Manager.Query
{
    using AutoMapper;
    using Framework.DataAccess.Repository;
    using Framework.Entity;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="QueryManager{TReadOnlyDbContext, TEntity, TReadModel}" />.
    /// </summary>
    /// <typeparam name="TReadOnlyDbContext">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    /// <typeparam name="TReadModel">.</typeparam>
    public abstract class QueryManager<TEntity, TReadModel> : BaseQueryManager<TEntity>, IQueryManager<TReadModel>
        where TEntity : BaseEntity
        where TReadModel : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryManager{TReadOnlyDbContext, TEntity, TReadModel}"/> class.
        /// </summary>
        /// <param name="queryGenericRepository">The queryGenericRepository<see cref="IGenericQueryRepository{TReadOnlyDbContext, TEntity}"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{QueryManager{TReadOnlyDbContext, TEntity, TReadModel}}"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        protected QueryManager(IGenericQueryRepository<TEntity> queryGenericRepository, ILogger<QueryManager<TEntity, TReadModel>> logger, IMapper mapper)
            : base(queryGenericRepository, logger, mapper)
        {
        }

        /// <summary>
        /// The GetItemAsync.
        /// </summary>
        /// <param name="key">The id<see cref="TKey"/>.</param>        
        /// <returns>The <see cref="Task{IEnumerable{TReadModel}}"/>.</returns>
        public virtual async Task<TReadModel> GetByKey<TKey>(TKey key, CancellationToken cancellationToken)
        {
            var readModels = new TReadModel();
            var data = await QueryRepository.GetByKey(key, cancellationToken);
            var models = Mapper.Map(data, readModels);
            await QueryAfterMapAsync(new[] { models }, new[] { data }).ConfigureAwait(false);
            return models;
        }

        /// <summary>
        /// The GSI1QueryAllAsync.
        /// </summary>
        /// <returns>The <see cref="Task{IEnumerable{TReadModel}}"/>.</returns>
        public virtual async Task<IEnumerable<TReadModel>> GetAll(CancellationToken cancellationToken)
        {
            var readModels = new List<TReadModel>();
            var data = await QueryRepository.GetAll(cancellationToken);
            var models = Mapper.Map(data, readModels);
            await QueryAfterMapAsync(models, data).ConfigureAwait(false);
            return models;
        }

        /// <summary>
        /// The QueryAfterMapAsync.
        /// </summary>
        /// <param name="models">The models<see cref="IEnumerable{TReadModel}"/>.</param>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        protected virtual Task QueryAfterMapAsync(IEnumerable<TReadModel> models, IEnumerable<TEntity> entities)
        {
            return Task.CompletedTask;
        }
    }
}
