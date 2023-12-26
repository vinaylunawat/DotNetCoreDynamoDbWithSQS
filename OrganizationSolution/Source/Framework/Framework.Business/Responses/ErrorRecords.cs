﻿namespace Framework.Business
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ErrorRecords{TErrorCode}" />.
    /// </summary>
    /// <typeparam name="TErrorCode">.</typeparam>
    public sealed class ErrorRecords<TKey, TErrorCode> : WrapperObject<ErrorRecord<TKey, TErrorCode>>
        where TErrorCode : struct, Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRecords{TErrorCode}"/> class.
        /// </summary>
        public ErrorRecords()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRecords{TErrorCode}"/> class.
        /// </summary>
        /// <param name="models">The models<see cref="IEnumerable{ErrorRecord{TErrorCode}}"/>.</param>
        public ErrorRecords(IEnumerable<ErrorRecord<TKey, TErrorCode>> models)
            : base(models)
        {
        }         
    }
}
