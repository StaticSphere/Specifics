#region Using Directives
using System;
using System.Linq.Expressions;
using System.Reflection;
using StaticSphere.Specifics.Properties;
#endregion

namespace StaticSphere.Specifics.Fluent
{
    /// <summary>
    /// Defines the fluent API method that can be enacted on a string property.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that is being validated.</typeparam>
    public class FluentStringRuleSet<TEntity> : FluentRuleSet<TEntity>
        where TEntity : class
    {
        #region Construction
        public FluentStringRuleSet(EntityRuleSet<TEntity> entityRules, Expression<Func<TEntity, string>> property)
        {
            if (entityRules == null)
                throw new ArgumentNullException("entityRules");
            if (property == null)
                throw new ArgumentNullException("property");

            EntityRules = entityRules;
            var body = property.Body as MemberExpression;
            if (body == null || (Property = body.Member as PropertyInfo) == null)
                throw new ArgumentException(Resources.InvalidPropertyError);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a rule that specifies that the string property must be set and not empty.
        /// </summary>
        /// <param name="messageLambda">A lambda or function that will determine the error message to use should this validation rule fail.</param>
        /// <returns>A <see cref="FluentStringRuleSet{TEntity}"/> object that can be used to chain string rules together.</returns>
        public FluentStringRuleSet<TEntity> Required(Func<TEntity, string> messageLambda)
        {
            var name = String.Format(Resources.FluentIsRequiredName, Property.Name);
            EntityRules.AddRule(x => !String.IsNullOrEmpty((string)Property.GetValue(x, null)), name, messageLambda);

            return this;
        }

        /// <summary>
        /// Adds a rule that specifies that the string property must be set and not empty.
        /// </summary>
        /// <param name="message">The error message to use should this validation rule fail.</param>
        /// <returns>A <see cref="FluentStringRuleSet{TEntity}"/> object that can be used to chain string rules together.</returns>
        public FluentStringRuleSet<TEntity> Required(string message)
        {
            return Required(x => message);
        }

        /// <summary>
        /// Adds a rule that specifies that the string property must be set and not empty.
        /// </summary>
        /// <returns>A <see cref="FluentStringRuleSet{TEntity}"/> object that can be used to chain string rules together.</returns>
        public FluentStringRuleSet<TEntity> Required()
        {
            return Required(String.Format(Resources.FluentIsRequiredDefaultMessage, Property.Name));
        }
        #endregion
    }
}