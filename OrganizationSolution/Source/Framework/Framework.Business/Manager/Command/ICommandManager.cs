namespace Framework.Business.Manager.Command
{
    using Framework.Business.Manager;
    using Framework.Business.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ICommandManager{TErrorCode, TCreateModel, TUpdateModel}" />.
    /// </summary>
    /// <typeparam name="TErrorCode">.</typeparam>
    /// <typeparam name="TCreateModel">.</typeparam>
    /// <typeparam name="TUpdateModel">.</typeparam>
    public interface ICommandManager<TKey, TErrorCode, TCreateModel, TUpdateModel>
         : IManagerBase
         where TErrorCode : struct, Enum
         where TCreateModel : class
         where TUpdateModel : class, TCreateModel, IModel
    {

        /// <summary>
        /// The CreateAsync.
        /// </summary>
        /// <param name="models">The models<see cref="IEnumerable{TCreateModel}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        Task<ManagerResponse<TKey, TErrorCode>> CreateAsync(TCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// The UpdateAsync.
        /// </summary>
        /// <param name="models">The models<see cref="IEnumerable{TUpdateModel}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        Task<ManagerResponse<TKey, TErrorCode>> UpdateAsync(TUpdateModel model, CancellationToken cancellationToken);


        /// <summary>
        /// The DeleteByIdAsync.
        /// </summary>
        /// <param name="ids">The ids<see cref="IEnumerable{long}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        Task<ManagerResponse<TKey, TErrorCode>> DeleteByKeyAsync(TKey key, CancellationToken cancellationToken);
    }
}
