namespace Framework.Business.Manager.Command
{
    using AutoMapper;
    using EnsureThat;
    using Framework.Business;
    using Framework.Business.Extension;
    using Framework.Business.Models;
    using Framework.Business.Models.Models;
    using Framework.DataAccess;
    using Framework.DataAccess.Repository;
    using Framework.Entity;
    using Framework.Entity.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CommandManager{TDbContext, TReadOnlyDbContext, TErrorCode, TEntity, TCreateModel, TUpdateModel}" />.
    /// </summary>
    /// <typeparam name="TDbContext">.</typeparam>
    /// <typeparam name="TReadOnlyDbContext">.</typeparam>
    /// <typeparam name="TErrorCode">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    /// <typeparam name="TCreateModel">.</typeparam>
    /// <typeparam name="TUpdateModel">.</typeparam>
    public abstract class CommandManager<TKey, TErrorCode, TEntity, TCreateModel, TUpdateModel> : ManagerBase, ICommandManager<TKey, TErrorCode, TCreateModel, TUpdateModel>
        where TErrorCode : struct, Enum
        where TEntity : class, IEntityWithId<TKey>, new()
        where TCreateModel : class, IModel
        where TUpdateModel : class, TCreateModel, IModelWithKey<TKey>
    {
        /// <summary>
        /// Defines the _queryRepository.
        /// </summary>
        private readonly IGenericQueryRepository<TEntity> _queryRepository;

        /// <summary>
        /// Defines the _commandRepository.
        /// </summary>
        private readonly IGenericCommandRepository<TEntity> _commandRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManager{TDbContext, TReadOnlyDbContext, TErrorCode, TEntity, TCreateModel, TUpdateModel}"/> class.
        /// </summary>
        /// <param name="queryRepository">The queryRepository<see cref="IGenericQueryRepository{TReadOnlyDbContext, TEntity}"/>.</param>
        /// <param name="commandRepository">The commandRepository<see cref="IGenericCommandRepository{TDbContext, TEntity}"/>.</param>
        /// <param name="createModelValidator">The createModelValidator<see cref="ModelValidator{TCreateModel}"/>.</param>
        /// <param name="updateModelValidator">The updateModelValidator<see cref="ModelValidator{TUpdateModel}"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{CommandManager{TDbContext, TReadOnlyDbContext, TErrorCode, TEntity, TCreateModel, TUpdateModel}}"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        /// <param name="idDoesNotExist">The idDoesNotExist<see cref="TErrorCode"/>.</param>
        /// <param name="idNotUnique">The idNotUnique<see cref="TErrorCode"/>.</param>
        protected CommandManager(IGenericQueryRepository<TEntity> queryRepository,
            IGenericCommandRepository<TEntity> commandRepository, ModelValidator<TCreateModel> createModelValidator, ModelValidator<TUpdateModel> updateModelValidator, ILogger<CommandManager<TKey, TErrorCode, TEntity, TCreateModel, TUpdateModel>> logger, IMapper mapper, TErrorCode idDoesNotExist, TErrorCode idNotUnique)
            : base(logger)
        {
            EnsureArg.IsNotNull(createModelValidator, nameof(createModelValidator));
            EnsureArg.IsNotNull(updateModelValidator, nameof(updateModelValidator));
            EnsureArg.IsNotNull(mapper, nameof(mapper));

            CreateModelValidator = createModelValidator;
            UpdateModelValidator = updateModelValidator;
            Mapper = mapper;
            IdNotUnique = idNotUnique;
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
        }

        /// <summary>
        /// Gets the IdNotUnique.
        /// </summary>
        protected TErrorCode IdNotUnique { get; }

        /// <summary>
        /// Gets the Mapper.
        /// </summary>
        protected IMapper Mapper { get; }

        /// <summary>
        /// Gets the CreateModelValidator.
        /// </summary>
        protected ModelValidator<TCreateModel> CreateModelValidator { get; }

        /// <summary>
        /// Gets the UpdateModelValidator.
        /// </summary>
        protected ModelValidator<TUpdateModel> UpdateModelValidator { get; }

        /// <summary>
        /// The CreateAsync.
        /// </summary>
        /// <param name="models">The models<see cref="IEnumerable{TCreateModel}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        public virtual async Task<ManagerResponse<TKey, TErrorCode>> CreateAsync(TCreateModel model, CancellationToken cancellationToken)
        {
            try
            {
                var errorRecords = CreateModelValidator.ExecuteCreateValidation<TKey, TErrorCode, TCreateModel>(model);
                var customErrorRecords = await CreateValidationAsync(model, cancellationToken).ConfigureAwait(false);

                return await CreateOrUpdateAsync(model, errorRecords, customErrorRecords, async entity =>
                {
                    await CreateAfterMapAsync(model, entity).ConfigureAwait(false);
                    await _commandRepository.CreateAsync(entity, cancellationToken).ConfigureAwait(false);
                    await CreateAfterSaveAsync(model, entity, cancellationToken).ConfigureAwait(false);
                }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(CreateAsync));
                return new ManagerResponse<TKey, TErrorCode>(ex);
            }
        }

        /// <summary>
        /// The UpdateAsync.
        /// </summary>
        /// <param name="models">The models<see cref="IEnumerable{TUpdateModel}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        public virtual async Task<ManagerResponse<TKey, TErrorCode>> UpdateAsync(TUpdateModel model, CancellationToken cancellationToken)
        {
            try
            {
                var errorRecords = UpdateModelValidator.ExecuteUpdateValidation<TKey, TErrorCode, TUpdateModel>(model);
                var customErrorRecords = await UpdateValidationAsync(model, cancellationToken).ConfigureAwait(false);

                return await CreateOrUpdateAsync(model, errorRecords, customErrorRecords, async entity =>
                {
                    await UpdateAfterMapAsync(model, entity).ConfigureAwait(false);
                    await _commandRepository.UpdateAsync(entity, cancellationToken).ConfigureAwait(false);
                    await UpdateAfterSaveAsync(model, entity, cancellationToken).ConfigureAwait(false);
                }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(UpdateAsync));
                return new ManagerResponse<TKey, TErrorCode>(ex);
            }
        }

        /// <summary>
        /// The DeleteByIdAsync.
        /// </summary>
        /// <param name="ids">The ids<see cref="IEnumerable{long}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        public virtual async Task<ManagerResponse<TKey, TErrorCode>> DeleteByKeyAsync(TKey key, CancellationToken cancellationToken)
        {
            try
            {
                var customErrorRecords = await DeleteValidationAsync(key, cancellationToken).ConfigureAwait(false);
                if (customErrorRecords.Any())
                {
                    return new ManagerResponse<TKey, TErrorCode>(customErrorRecords);
                }
                await _commandRepository.DeleteAsync(key, cancellationToken);
                return new ManagerResponse<TKey, TErrorCode>(key);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(DeleteByKeyAsync));
                return new ManagerResponse<TKey, TErrorCode>(ex);
            }
        }

        /// <summary>
        /// The CreateValidationAsync.
        /// </summary>
        /// <param name="indexedModels">The indexedModels<see cref="IList{IIndexedItem{TCreateModel}}"/>.</param>
        /// <returns>The <see cref="Task{ErrorRecords{TErrorCode}}"/>.</returns>
        protected virtual async Task<ErrorRecords<TKey, TErrorCode>> CreateValidationAsync(TCreateModel model, CancellationToken cancellationToken)
        {
            Logger.LogDebug($"Calling {nameof(CreateValidationAsync)}");

            return await Task.FromResult(new ErrorRecords<TKey, TErrorCode>()).ConfigureAwait(false);
        }

        /// <summary>
        /// The UpdateValidationAsync.
        /// </summary>
        /// <param name="indexedModels">The indexedModels<see cref="IList{IIndexedItem{TUpdateModel}}"/>.</param>
        /// <returns>The <see cref="Task{ErrorRecords{TErrorCode}}"/>.</returns>
        protected virtual async Task<ErrorRecords<TKey, TErrorCode>> UpdateValidationAsync(TUpdateModel updateModel, CancellationToken cancellationToken)
        {
            Logger.LogDebug($"Calling {nameof(UpdateValidationAsync)}");
            return await Task.FromResult(new ErrorRecords<TKey, TErrorCode>()).ConfigureAwait(false);
        }

        protected virtual async Task<ErrorRecords<TKey, TErrorCode>> DeleteValidationAsync(TKey key, CancellationToken cancellationToken)
        {
            Logger.LogDebug($"Calling {nameof(DeleteValidationAsync)}");
            return await Task.FromResult(new ErrorRecords<TKey, TErrorCode>()).ConfigureAwait(false);
        }

        /// <summary>
        /// The CreateAfterMapAsync.
        /// </summary>
        /// <param name="indexedItems">The indexedItems<see cref="IList{IIndexedItem{TCreateModel}}"/>.</param>
        /// <param name="entities">The entities<see cref="IList{TEntity}"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        protected virtual Task CreateAfterMapAsync(TCreateModel model, TEntity entity)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// The UpdateAfterMapAsync.
        /// </summary>
        /// <param name="indexedItems">The indexedItems<see cref="IList{IIndexedItem{TUpdateModel}}"/>.</param>
        /// <param name="entities">The entities<see cref="IList{TEntity}"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        protected virtual Task UpdateAfterMapAsync(TUpdateModel model, TEntity entity)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// The CreateOrUpdateAsync.
        /// </summary>
        /// <typeparam name="TModel">.</typeparam>
        /// <param name="models">The models<see cref="IEnumerable{TModel}"/>.</param>
        /// <param name="errorRecords">The errorRecords<see cref="ErrorRecords{TErrorCode}"/>.</param>
        /// <param name="customErrorRecords">The customErrorRecords<see cref="IEnumerable{ErrorRecord{TErrorCode}}"/>.</param>
        /// <param name="entityDatabaseOperation">The entityDatabaseOperation<see cref="Func{List{TEntity}, Task}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        private async Task<ManagerResponse<TKey, TErrorCode>> CreateOrUpdateAsync<TModel>(TModel model, ErrorRecords<TKey, TErrorCode> errorRecords, IEnumerable<ErrorRecord<TKey, TErrorCode>> customErrorRecords, Func<TEntity, Task> entityDatabaseOperation)
        {
            var mergedErrorRecords = errorRecords.Merge(customErrorRecords);

            if (mergedErrorRecords.Any())
            {
                return new ManagerResponse<TKey, TErrorCode>(mergedErrorRecords);
            }

            var entity = new TEntity();
            Mapper.Map(model, entity);
            await entityDatabaseOperation(entity).ConfigureAwait(false);
            return new ManagerResponse<TKey, TErrorCode>(entity.Id);
        }

        protected virtual Task CreateAfterSaveAsync(TCreateModel model, TEntity entity, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected virtual Task UpdateAfterSaveAsync(TUpdateModel model, TEntity entity, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
