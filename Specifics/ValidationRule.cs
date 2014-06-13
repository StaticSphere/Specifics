#region Using Directives
using System;
using System.Linq.Expressions;
#endregion

namespace StaticSphere.Specifics
{
    /// <summary>
    /// Defines a validation rule that describes what a valid entity looks like.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that the validation rule will apply to.</typeparam>
    public class ValidationRule<TEntity>
        where TEntity : class
    {
        #region Private Member Variables
        private Specification<TEntity> _specification;
        private Func<TEntity, string> _errorMessageLambda;
        #endregion

        #region Properties
        /// <summary>
        /// The name of the rule.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The message given when this rule is not valid.
        /// </summary>
        /// <remarks>
        /// This property is set when the <see cref="Validate"/> method is called. It is null until then.
        /// </remarks>
        public string ErrorMessage { get; private set; }
        #endregion

        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule{TEntity}"/> class.
        /// </summary>
        /// <param name="specification">The specification that this business rule will apply when determining validity of the tested entity.</param>
        /// <param name="name">The name of the rule.</param>
        public ValidationRule(Specification<TEntity> specification, string name)
        {
            if (specification == null)
                throw new ArgumentNullException("specification");
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            _specification = specification;
            Name = name;
            _errorMessageLambda = msg => String.Format("The rule named {0} failed validation.", Name);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule{TEntity}"/> class.
        /// </summary>
        /// <param name="expression">A function or lambda that this business rule will apply when determining validity of the tested entity.</param>
        /// <param name="name">The name of the rule.</param>
        public ValidationRule(Expression<Func<TEntity, bool>> expression, string name)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            _specification = new Specification<TEntity>(expression);
            Name = name;
            _errorMessageLambda = msg => String.Format("The rule named {0} failed validation.", Name);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule{TEntity}"/> class.
        /// </summary>
        /// <param name="specification">The specification that this business rule will apply when determining validity of the tested entity.</param>
        /// <param name="name">The name of the rule.</param>
        /// <param name="errorMessage">The error message that will be given if this validation rule determines that the tested entity is invalid.</param>
        /// <exception cref="System.ArgumentNullException">Raised if the provided specification is null.</exception>
        public ValidationRule(Specification<TEntity> specification, string name, string errorMessage)
        {
            if (specification == null)
                throw new ArgumentNullException("specification");
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (String.IsNullOrEmpty(errorMessage))
                throw new ArgumentNullException("errorMessage");

            _specification = specification;
            Name = name;
            _errorMessageLambda = msg => errorMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule{TEntity}"/> class.
        /// </summary>
        /// <param name="specification">The specification that this business rule will apply when determining validity of the tested entity.</param>
        /// <param name="name">The name of the rule.</param>
        /// <param name="errorMessageLambda">A function or lambda that will be used to create the error message for this rule.</param>
        /// <exception cref="System.ArgumentNullException">Raised if the provided specification is null.</exception>
        public ValidationRule(Specification<TEntity> specification, string name, Func<TEntity, string> errorMessageLambda)
        {
            if (specification == null)
                throw new ArgumentNullException("specification");
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (errorMessageLambda == null)
                throw new ArgumentNullException("errorMessageLambda");

            _specification = specification;
            Name = name;
            _errorMessageLambda = errorMessageLambda;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule{TEntity}"/> class.
        /// </summary>
        /// <param name="expression">A function or lambda that this business rule will apply when determining validity of the tested entity.</param>
        /// <param name="name">The name of the rule.</param>
        /// <param name="errorMessage">The error message that will be given if this validation rule determines that the tested entity is invalid.</param>
        /// <exception cref="System.ArgumentNullException">Raised if the provided specification is null.</exception>
        public ValidationRule(Expression<Func<TEntity, bool>> expression, string name, string errorMessage)
        {
            if (expression == null)
                throw new ArgumentNullException("specification");
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (String.IsNullOrEmpty(errorMessage))
                throw new ArgumentNullException("errorMessage");

            _specification = new Specification<TEntity>(expression);
            Name = name;
            _errorMessageLambda = msg => errorMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule{TEntity}"/> class.
        /// </summary>
        /// <param name="expression">A function or lambda that this business rule will apply when determining validity of the tested entity.</param>
        /// <param name="name">The name of the rule.</param>
        /// <param name="errorMessageLambda">A function or lambda that will be used to create the error message for this rule.</param>
        /// <exception cref="System.ArgumentNullException">Raised if the provided specification is null.</exception>
        public ValidationRule(Expression<Func<TEntity, bool>> expression, string name, Func<TEntity, string> errorMessageLambda)
        {
            if (expression == null)
                throw new ArgumentNullException("specification");
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (errorMessageLambda == null)
                throw new ArgumentNullException("errorMessageLambda");

            _specification = new Specification<TEntity>(expression);
            Name = name;
            _errorMessageLambda = errorMessageLambda;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Validates the provided entity against the business rule assigned to this object.
        /// </summary>
        /// <param name="entity">The entity to validate.</param>
        /// <returns><c>true</c> if the provided entity satisfies the assigned rule; <c>false</c> otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">Raised if the provided entity is null.</exception>
        public bool Validate(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var valid = _specification.IsSatisfiedBy(entity);
            ErrorMessage = valid ? null : _errorMessageLambda.Invoke(entity);
            return valid;
        }
        #endregion
    }
}