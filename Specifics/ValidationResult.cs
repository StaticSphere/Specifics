#region Using Directives
using System;
using System.Collections.Generic;
#endregion

namespace StaticSphere.Specifics
{
    /// <summary>
    /// Provides information about how well an entity satisfies a specification.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that this <see cref="ValidationResult{TEntity}"/> represents.</typeparam>
    public class ValidationResult<TEntity>
        where TEntity : class
    {
        #region Private Member Variables
        private TEntity _entity;
        private List<ValidationError> _errors;
        #endregion

        #region Properties
        /// <summary>
        /// Specifies whether a validation was successful or not.
        /// </summary>
        public bool Valid { get { return _errors.Count == 0; } }

        /// <summary>
        /// The list of <see cref="ValidationError"/> objects created during validation.
        /// </summary>
        public IEnumerable<ValidationError> Errors
        {
            get
            {
                foreach (var error in _errors)
                    yield return error;
            }
        }

        /// <summary>
        /// The entity that this <see cref="ValidationResult{TEntity}"/> represents.
        /// </summary>
        public TEntity Entity { get { return _entity; } }
        #endregion

        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult{TEntity}"/> class.
        /// </summary>
        /// <param name="entity">The entity that this <see cref="ValidationResult{TEntity}"/> represents.</param>
        public ValidationResult(TEntity entity)
        {
            _entity = entity;
            _errors = new List<ValidationError>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds the given <see cref="ValidationError"/> instance to the error collection of this result.
        /// </summary>
        /// <param name="error">The <see cref="ValidationError"/> instance to add.</param>
        /// <returns>The count of errors in this result after adding the given <see cref="ValidationError"/> instance.</returns>
        public int AddError(ValidationError error)
        {
            if (error == null)
                throw new ArgumentNullException("error");
            if (_errors.Contains(error))
                throw new InvalidOperationException("This validation error object is already in the errors collection.");

            _errors.Add(error);
            return _errors.Count;
        }
        #endregion
    }
}