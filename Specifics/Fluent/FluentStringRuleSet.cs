#region Using Directives
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="FluentStringRuleSet{TEntity}"/> class.
        /// </summary>
        /// <param name="entityRules">The <see cref="EntityRuleSet{TEntity}"/> instance that validation rules will be added to.</param>
        /// <param name="property">An expression that represents the property that this fluent chain applies to.</param>
        /// <exception cref="System.ArgumentNullException">
        /// entityRules
        /// or
        /// property
        /// </exception>
        /// <exception cref="System.ArgumentException">Raised if the property parameter does not represent a property.</exception>
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

        /// <summary>
        /// Adds a rule that specifies that the string property must be at least a specified length.
        /// </summary>
        /// <param name="minimumLength">The minimum length that the string property can be.</param>
        /// <param name="messageLambda">A lambda or function that will determine the error message to use should this validation rule fail.</param>
        /// <returns>A <see cref="FluentStringRuleSet{TEntity}"/> object that can be used to chain string rules together.</returns>
        public FluentStringRuleSet<TEntity> MinimumLength(int minimumLength, Func<TEntity, string> messageLambda)
        {
            var name = String.Format(Resources.FluentMinimumLengthName, Property.Name);
            EntityRules.AddRule(x => ((string)Property.GetValue(x, null)) == null || ((string)Property.GetValue(x, null)).Length >= minimumLength, name, messageLambda);

            return this;
        }

        /// <summary>
        /// Adds a rule that specifies that the string property must be at least a specified length.
        /// </summary>
        /// <param name="minimumLength">The minimum length that the string property can be.</param>
        /// <param name="message">The error message to use should this validation rule fail.</param>
        /// <returns>A <see cref="FluentStringRuleSet{TEntity}"/> object that can be used to chain string rules together.</returns>
        public FluentStringRuleSet<TEntity> MinimumLength(int minimumLength, string message)
        {
            return MinimumLength(minimumLength, x => message);
        }

        /// <summary>
        /// Adds a rule that specifies that the string property must be at least a specified length.
        /// </summary>
        /// <param name="minimumLength">The minimum length that the string property can be.</param>
        /// <returns>A <see cref="FluentStringRuleSet{TEntity}"/> object that can be used to chain string rules together.</returns>
        public FluentStringRuleSet<TEntity> MinimumLength(int minimumLength)
        {
            return MinimumLength(minimumLength, String.Format(Resources.FluentMinimumLengthDefaultMessage, Property.Name, minimumLength));
        }

        /// <summary>
        /// Adds a rule that specifies that the string property must be at most a specified length.
        /// </summary>
        /// <param name="maximumLength">The maximum length that the string property can be.</param>
        /// <param name="messageLambda">A lambda or function that will determine the error message to use should this validation rule fail.</param>
        /// <returns>A <see cref="FluentStringRuleSet{TEntity}"/> object that can be used to chain string rules together.</returns>
        public FluentStringRuleSet<TEntity> MaximumLength(int maximumLength, Func<TEntity, string> messageLambda)
        {
            var name = String.Format(Resources.FluentMaximumLengthName, Property.Name);
            EntityRules.AddRule(x => ((string)Property.GetValue(x, null)) == null || ((string)Property.GetValue(x, null)).Length <= maximumLength, name, messageLambda);

            return this;
        }

        /// <summary>
        /// Adds a rule that specifies that the string property must be at most a specified length.
        /// </summary>
        /// <param name="maximumLength">The maximum length that the string property can be.</param>
        /// <param name="message">The error message to use should this validation rule fail.</param>
        /// <returns>A <see cref="FluentStringRuleSet{TEntity}"/> object that can be used to chain string rules together.</returns>
        public FluentStringRuleSet<TEntity> MaximumLength(int maximumLength, string message)
        {
            return MaximumLength(maximumLength, x => message);
        }

        /// <summary>
        /// Adds a rule that specifies that the string property must be at most a specified length.
        /// </summary>
        /// <param name="maximumLength">The maximum length that the string property can be.</param>
        /// <returns>A <see cref="FluentStringRuleSet{TEntity}"/> object that can be used to chain string rules together.</returns>
        public FluentStringRuleSet<TEntity> MaximumLength(int maximumLength)
        {
            return MaximumLength(maximumLength, String.Format(Resources.FluentMaximumLengthDefaultMessage, Property.Name, maximumLength));
        }

        /// <summary>
        /// Adds a rule that specifies that the string property must match a specific regular expression pattern.
        /// </summary>
        /// <param name="pattern">The regular expression pattern that the property must match.</param>
        /// <param name="messageLambda">A lambda or function that will determine the error message to use should this validation rule fail.</param>
        /// <returns>A <see cref="FluentStringRuleSet{TEntity}"/> object that can be used to chain string rules together.</returns>
        public FluentStringRuleSet<TEntity> MatchesPattern(string pattern, Func<TEntity, string> messageLambda)
        {
            var name = String.Format(Resources.FluentMatchesPatternName, Property.Name);
            EntityRules.AddRule(x => ((string)Property.GetValue(x, null)) == null || Regex.IsMatch((string)Property.GetValue(x, null), pattern), name, messageLambda);

            return this;
        }

        /// <summary>
        /// Adds a rule that specifies that the string property must match a specific regular expression pattern.
        /// </summary>
        /// <param name="pattern">The regular expression pattern that the property must match.</param>
        /// <param name="message">The error message to use should this validation rule fail.</param>
        /// <returns>A <see cref="FluentStringRuleSet{TEntity}"/> object that can be used to chain string rules together.</returns>
        public FluentStringRuleSet<TEntity> MatchesPattern(string pattern, string message)
        {
            return MatchesPattern(pattern, x => message);
        }

        /// <summary>
        /// Adds a rule that specifies that the string property must match a specific regular expression pattern.
        /// </summary>
        /// <param name="pattern">The regular expression pattern that the property must match.</param>
        /// <returns>A <see cref="FluentStringRuleSet{TEntity}"/> object that can be used to chain string rules together.</returns>
        public FluentStringRuleSet<TEntity> MatchesPattern(string pattern)
        {
            return MatchesPattern(pattern, String.Format(Resources.FluentMatchesPatternDefaultMessage, Property.Name, pattern));
        }
        #endregion
    }
}