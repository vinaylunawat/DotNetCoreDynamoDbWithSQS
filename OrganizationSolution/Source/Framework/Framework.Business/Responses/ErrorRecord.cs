﻿namespace Framework.Business
{
    using EnsureThat;
    using FluentValidation.Results;
    using Framework.Business.Extension;
    using System;
    using System.Collections.Generic;    
    using System.Text;

    /// <summary>
    /// Defines the <see cref="ErrorRecord{TErrorCode}" />.
    /// </summary>
    /// <typeparam name="TErrorCode">.</typeparam>
    public class ErrorRecord<TKey, TErrorCode>
        where TErrorCode : struct, Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRecord{TErrorCode}"/> class.
        /// </summary>
        /// <param name="ordinalPosition">The ordinalPosition<see cref="int"/>.</param>
        /// <param name="validationResults">The validationResults<see cref="ValidationResult"/>.</param>
        public ErrorRecord(int ordinalPosition, ValidationResult validationResults)
        {
            EnsureArg.IsGte(ordinalPosition, 0, nameof(ordinalPosition));
            EnsureArg.IsNotNull(validationResults, nameof(validationResults));

            SetValues(default, ordinalPosition, validationResults.ToErrorMessages<TErrorCode>());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRecord{TErrorCode}"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="ordinalPosition">The ordinalPosition<see cref="int"/>.</param>
        /// <param name="validationResults">The validationResults<see cref="ValidationResult"/>.</param>
        public ErrorRecord(TKey key, int ordinalPosition, ValidationResult validationResults)
        {
            EnsureArg.IsGte(ordinalPosition, 0, nameof(ordinalPosition));
            EnsureArg.IsNotNull(validationResults, nameof(validationResults));

            SetValues(key, ordinalPosition, validationResults.ToErrorMessages<TErrorCode>());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRecord{TErrorCode}"/> class.
        /// </summary>
        /// <param name="ordinalPosition">The ordinalPosition<see cref="int"/>.</param>
        /// <param name="errorMessages">The errorMessages<see cref="ErrorMessages{TErrorCode}"/>.</param>
        public ErrorRecord(int ordinalPosition, ErrorMessages<TErrorCode> errorMessages)
        {
            EnsureArg.IsGte(ordinalPosition, 0, nameof(ordinalPosition));
            EnsureArg.IsNotNull(errorMessages, nameof(errorMessages));

            SetValues(default, ordinalPosition, errorMessages);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRecord{TErrorCode}"/> class.
        /// </summary>
        /// <param name="ordinalPosition">The ordinalPosition<see cref="int"/>.</param>
        /// <param name="errorMessage">The errorMessage<see cref="ErrorMessage{TErrorCode}"/>.</param>
        public ErrorRecord(int ordinalPosition, ErrorMessage<TErrorCode> errorMessage)
        {
            EnsureArg.IsGte(ordinalPosition, 0, nameof(ordinalPosition));
            EnsureArg.IsNotNull(errorMessage, nameof(errorMessage));

            var errorMessages = new ErrorMessages<TErrorCode>() { errorMessage };
            SetValues(default, ordinalPosition, errorMessages);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRecord{TErrorCode}"/> class.
        /// </summary>
        /// <param name="ordinalPosition">The ordinalPosition<see cref="int"/>.</param>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public ErrorRecord(int ordinalPosition, TErrorCode errorCode, string message)
        {
            EnsureArg.IsGte(ordinalPosition, 0, nameof(ordinalPosition));
            EnsureArg.IsNotNull<TErrorCode>(errorCode, nameof(errorCode));
            EnsureArg.IsNotEmptyOrWhiteSpace(message, nameof(message));

            var errorMessages = new ErrorMessages<TErrorCode>
            {
                new ErrorMessage<TErrorCode>(errorCode, message)
            };

            SetValues(default, ordinalPosition, errorMessages);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRecord{TErrorCode}"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="ordinalPosition">The ordinalPosition<see cref="int"/>.</param>
        /// <param name="errorMessages">The errorMessages<see cref="ErrorMessages{TErrorCode}"/>.</param>
        public ErrorRecord(TKey key, int ordinalPosition, ErrorMessages<TErrorCode> errorMessages)
        {
            EnsureArg.IsGte(ordinalPosition, 0, nameof(ordinalPosition));
            EnsureArg.IsNotNull(errorMessages, nameof(errorMessages));

            SetValues(key, ordinalPosition, errorMessages);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRecord{TErrorCode}"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="ordinalPosition">The ordinalPosition<see cref="int"/>.</param>
        /// <param name="errorMessage">The errorMessage<see cref="ErrorMessage{TErrorCode}"/>.</param>
        public ErrorRecord(TKey key, int ordinalPosition, ErrorMessage<TErrorCode> errorMessage)
        {
            EnsureArg.IsGte(ordinalPosition, 0, nameof(ordinalPosition));
            EnsureArg.IsNotNull(errorMessage, nameof(errorMessage));

            var errorMessages = new ErrorMessages<TErrorCode>() { errorMessage };
            SetValues(key, ordinalPosition, errorMessages);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRecord{TErrorCode}"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="ordinalPosition">The ordinalPosition<see cref="int"/>.</param>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public ErrorRecord(TKey key, int ordinalPosition, TErrorCode errorCode, string message)
        {
            EnsureArg.IsGte(ordinalPosition, 0, nameof(ordinalPosition));
            EnsureArg.IsNotNull<TErrorCode>(errorCode, nameof(errorCode));
            EnsureArg.IsNotEmptyOrWhiteSpace(message, nameof(message));

            var errorMessages = new ErrorMessages<TErrorCode>
            {
                new ErrorMessage<TErrorCode>(errorCode, message)
            };

            SetValues(key, ordinalPosition, errorMessages);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRecord{TErrorCode}"/> class.
        /// </summary>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        public ErrorRecord(Exception exception)
        {
            EnsureArg.IsNotNull(exception, nameof(exception));

            var errorMessages = new ErrorMessages<TErrorCode>
            {
                new ErrorMessage<TErrorCode>(exception)
            };

            SetValues(default, 0, errorMessages);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRecord{TErrorCode}"/> class.
        /// </summary>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        public ErrorRecord(TErrorCode errorCode, Exception exception)
        {
            EnsureArg.IsNotNull<TErrorCode>(errorCode, nameof(errorCode));
            EnsureArg.IsNotNull(exception, nameof(exception));

            var errorMessages = new ErrorMessages<TErrorCode>
            {
                new ErrorMessage<TErrorCode>(errorCode, exception)
            };

            SetValues(default, 0, errorMessages);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRecord{TErrorCode}"/> class.
        /// </summary>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public ErrorRecord(TErrorCode errorCode, string message)
        {
            EnsureArg.IsNotNull<TErrorCode>(errorCode, nameof(errorCode));
            EnsureArg.IsNotEmptyOrWhiteSpace(message, nameof(message));

            var errorMessages = new ErrorMessages<TErrorCode>
            {
                new ErrorMessage<TErrorCode>(errorCode, message)
            };

            SetValues(default, 0, errorMessages);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRecord{TErrorCode}"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="TKey"/>.</param>
        /// <param name="ordinalPosition">The ordinalPosition<see cref="int"/>.</param>
        /// <param name="errorMessages">The errorMessages<see cref="IEnumerable{ErrorMessage{TErrorCode}}"/>.</param>
        internal ErrorRecord(TKey id, int ordinalPosition, IEnumerable<ErrorMessage<TErrorCode>> errorMessages)
        {
            EnsureArg.IsNotNull(errorMessages, nameof(errorMessages));

            SetValues(id, ordinalPosition, new ErrorMessages<TErrorCode>(errorMessages));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRecord{TErrorCode}"/> class.
        /// </summary>
        protected ErrorRecord()
        {
        }

        /// <summary>
        /// Gets or sets the Id
        /// Gets the identifier..
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// Gets or sets the OrdinalPosition
        /// Gets the ordinal position..
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the Errors
        /// Gets the errors..
        /// </summary>
        public ErrorMessages<TErrorCode> Errors { get; set; }

        /// <summary>
        /// The ToFormattedString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        internal string ToFormattedString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Id: '{Key}'");
            stringBuilder.AppendLine($"OrdinalPosition: '{OrdinalPosition}'");
            stringBuilder.AppendLine($"Errors: ");

            foreach (var error in Errors)
            {
                stringBuilder.AppendLine($"        {error.ToFormattedString()}");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// The SetValues.
        /// </summary>
        /// <param name="id">The id<see cref="TKey"/>.</param>
        /// <param name="ordinalPosition">The ordinalPosition<see cref="int"/>.</param>
        /// <param name="errors">The errors<see cref="ErrorMessages{TErrorCode}"/>.</param>
        private void SetValues(TKey key, int ordinalPosition, ErrorMessages<TErrorCode> errors)
        {
            Key = key;
            OrdinalPosition = ordinalPosition;
            Errors = errors;
        }
    }
}
