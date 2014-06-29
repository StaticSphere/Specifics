#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using StaticSphere.Specifics.Fluent;
#endregion

namespace StaticSphere.Specifics
{
    /// <summary>
    /// Provides base functionality for describing the validation rules that determine if an entity
    /// meets a set of specifications.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that the rule set is being applied to.</typeparam>
    public abstract class EntityRuleSet<TEntity>
        where TEntity : class
    {
        #region Internal Properties
        internal Dictionary<string, ValidationRule<TEntity>> ValidationRules { get; private set; }
        #endregion

        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRuleSet{TEntity}"/> class.
        /// </summary>
        public EntityRuleSet()
        {
            ValidationRules = new Dictionary<string, ValidationRule<TEntity>>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a new validation rule to the rule set.
        /// </summary>
        /// <param name="rule">A <see cref="ValidationRule{TEntity}"/> instance that describes the rule to be added./></param>
        /// <returns>The number of rules currently in the rule set.</returns>
        public int AddRule(ValidationRule<TEntity> rule)
        {
            if (rule == null)
                throw new ArgumentNullException("rule");

            ValidationRules.Add(rule.Name, rule);
            return ValidationRules.Count;
        }

        /// <summary>
        /// Adds a new validation rule to the rule set.
        /// </summary>
        /// <param name="expression">A function or lambda that this business rule will apply when determining validity of the tested entity.</param>
        /// <param name="name">The name of the rule.</param>
        /// <returns>The number of rules currently in the rule set.</returns>
        public int AddRule(Expression<Func<TEntity, bool>> expression, string name)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            ValidationRules.Add(name, new ValidationRule<TEntity>(expression, name));
            return ValidationRules.Count;
        }

        /// <summary>
        /// Adds a new validation rule to the rule set.
        /// </summary>
        /// <param name="expression">A function or lambda that this business rule will apply when determining validity of the tested entity.</param>
        /// <param name="name">The name of the rule.</param>
        /// <param name="errorMessage">The error message that will be given if this validation rule determines that the tested entity is invalid.</param>
        /// <returns>The number of rules currently in the rule set.</returns>
        public int AddRule(Expression<Func<TEntity, bool>> expression, string name, string errorMessage)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (String.IsNullOrEmpty(errorMessage))
                throw new ArgumentNullException("errorMessage");

            ValidationRules.Add(name, new ValidationRule<TEntity>(expression, name, errorMessage));
            return ValidationRules.Count;
        }

        /// <summary>
        /// Adds a new validation rule to the rule set.
        /// </summary>
        /// <param name="expression">A function or lambda that this business rule will apply when determining validity of the tested entity.</param>
        /// <param name="name">The name of the rule.</param>
        /// <param name="errorMessageLambda">A function or lambda that will be used to create the error message for this rule.</param>
        /// <returns>The number of rules currently in the rule set.</returns>
        public int AddRule(Expression<Func<TEntity, bool>> expression, string name, Func<TEntity, string> errorMessageLambda)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            ValidationRules.Add(name, new ValidationRule<TEntity>(expression, name, errorMessageLambda));
            return ValidationRules.Count;
        }

        /// <summary>
        /// Validates the specified entity based on specifications.
        /// </summary>
        /// <param name="entity">The entity to validate.</param>
        /// <returns>A <see cref="ValidationResult{TEntity}"/> instance that describes how well the provided entity satisfies the specification.</returns>
        public ValidationResult<TEntity> Validate(TEntity entity)
        {
            var result = new ValidationResult<TEntity>(entity);

            foreach(var ruleName in ValidationRules.Keys)
            {
                var rule = ValidationRules[ruleName];
                if (!rule.Validate(entity))
                {
                    var validationError = new ValidationError(rule.Name, rule.ErrorMessage);
                    result.AddError(validationError);
                    if (entity is IValidatedEntity)
                    {
                        var valEntity = entity as IValidatedEntity;
                        if (valEntity.ValidationErrors == null)
                            throw new InvalidOperationException("When implementing IValidatedEntity, the ValidationErrors collection must be initialized.");

                        valEntity.ValidationErrors.Add(validationError);
                    }
                }
            }

            return result;
        }
        #endregion

        #region Fluent Api Methods
        /// <summary>
        /// Provides the ability to add validation rules against string properties with a fluent API.
        /// </summary>
        /// <param name="property">An expression that describes property being validated.</param>
        /// <returns>A <see cref="FluentStringRuleSet{TEntity}"/> object that will add validation rules based on the fluent API methods called.</returns>
        protected FluentStringRuleSet<TEntity> Property(Expression<Func<TEntity, string>> property)
        {
            return new FluentStringRuleSet<TEntity>(this, property);
        }
        #endregion
    }
}