namespace Framework.Business.Manager.Query
{
    using AutoMapper;
    using EnsureThat;
    using Framework.Business.Manager;
    using Framework.DataAccess.Repository;
    using Framework.Entity;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Defines the <see cref="BaseQueryManager{TReadOnlyDbContext, TEntity}" />.
    /// </summary>
    /// <typeparam name="TReadOnlyDbContext">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    public abstract class BaseQueryManager<TEntity> : ManagerBase
        where TEntity : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseQueryManager{TReadOnlyDbContext, TEntity}"/> class.
        /// </summary>
        /// <param name="queryGenericRepository">The queryGenericRepository<see cref="IGenericQueryRepository{TReadOnlyDbContext, TEntity}"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{BaseQueryManager{TKey, TEntity}}"/>.</param>
        protected BaseQueryManager(IGenericQueryRepository<TEntity> queryGenericRepository, ILogger<BaseQueryManager<TEntity>> logger, IMapper mapper)
            : base(logger)
        {
            EnsureArg.IsNotNull(queryGenericRepository, nameof(queryGenericRepository));
            EnsureArg.IsNotNull(mapper, nameof(mapper));
            Mapper = mapper;
            QueryRepository = queryGenericRepository;
        }

        /// <summary>
        /// Gets the QueryRepository.
        /// </summary>
        protected IGenericQueryRepository<TEntity> QueryRepository { get; private set; }

        protected IMapper Mapper { get; }
    }
}
