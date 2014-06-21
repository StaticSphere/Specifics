#region Using Directives
using System;
#endregion

namespace StaticSphere.Specifics
{
    /// <summary>
    /// Provides details on a validation error.
    /// </summary>
    public class ValidationError
    {
        #region Properties
        /// <summary>
        /// The name of the validation error.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The message that describes the validation error.
        /// </summary>
        public string Message { get; private set; }
        #endregion

        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class.
        /// </summary>
        /// <param name="name">The name of the <see cref="ValidationRule{TEntity}"/> that created this error.</param>
        /// <param name="message">The message that was generated with this error.</param>
        /// <exception cref="System.ArgumentNullException">
        /// name
        /// or
        /// message
        /// </exception>
        public ValidationError(string name, string message)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (String.IsNullOrEmpty(message))
                throw new ArgumentNullException("message");

            Name = name;
            Message = message;
        }
        #endregion
    }
}