namespace Framework.Business
{
    using EnsureThat;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="ManagerResponse{TErrorCode}" />.
    /// </summary>
    /// <typeparam name="TErrorCode">.</typeparam>
    public class ManagerResponse<TKey, TErrorCode> : ManagerResponseBase<TKey, TErrorCode>
        where TErrorCode : struct, Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponse{TErrorCode}"/> class.
        /// </summary>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public ManagerResponse(TErrorCode errorCode, string message)
            : base(errorCode, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponse{TErrorCode}"/> class.
        /// </summary>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        public ManagerResponse(TErrorCode errorCode, Exception exception)
            : base(errorCode, exception)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponse{TErrorCode}"/> class.
        /// </summary>
        /// <param name="errorRecords">The errorRecords<see cref="ErrorRecords{TErrorCode}"/>.</param>
        public ManagerResponse(ErrorRecords<TKey, TErrorCode> errorRecords)
            : base(errorRecords)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponse{TErrorCode}"/> class.
        /// </summary>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        public ManagerResponse(Exception exception)
            : base(exception)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponse{TErrorCode}"/> class.
        /// </summary>
        /// <param name="ids">The ids<see cref="IEnumerable{long}"/>.</param>
        public ManagerResponse(TKey key)
            : base()
        {

            Key = key;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponse{TErrorCode}"/> class.
        /// </summary>
        public ManagerResponse()
            : base()
        {

        }

        /// <summary>
        /// Gets or sets the Ids.
        /// </summary>
        public TKey Key { get; set; }
    }
}
